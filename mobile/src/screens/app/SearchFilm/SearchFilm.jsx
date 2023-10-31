import React, {useCallback, useState} from 'react';
import {Image, ScrollView, TouchableOpacity, View} from 'react-native';
import {showMessage} from 'react-native-flash-message';
import {
  Avatar,
  Button,
  DataTable,
  IconButton,
  MD2Colors,
  MD3Colors,
  Modal,
  Portal,
  Text,
  TextInput,
} from 'react-native-paper';
import {useSelector} from 'react-redux';
import {fetchSaveFilm, fetchSearchFilms} from '../../../services/APIService';
import styles from './SearchFilm.styles';
import SavedFilmStatus from '../../../enums/SavedFilmStatus';
import useCustomForm from '../../../hooks/useCustomForm';

function SearchFilm() {
  const initialStates = {
    modalState: {
      visible: false,
      selectedSearchedFilmId: null,
    },
    fetchResult: {
      loading: false,
      error: null,
      data: null,
    },
  };

  const {handleBlur, handleFocus, form, handleSetValueOfFormKey} =
    useCustomForm(['title']);

  const {user} = useSelector(state => state.auth);

  const [fetchResult, setFetchResult] = useState(initialStates.fetchResult);
  const [modalState, setModalState] = useState(initialStates.modalState);

  const toggleModal = () => {
    setModalState({...modalState, visible: !modalState.visible});
  };

  const clearModalState = () => {
    setModalState(initialStates.modalState);
  };
  const clearFetchState = () => {
    setFetchResult(initialStates.fetchResult);
  };

  const handleFetchSaveFilm = status => {
    fetchSaveFilm(
      modalState.selectedSearchedFilmId.toString(),
      user.userId,
      status,
    )
      .then(response => {
        clearFetchState();
        clearModalState();
      })
      .catch(error => {
        if (error.response && error.response.status === 400) {
          const {message} = error.response.data;
          setFetchResult({
            ...fetchResult,
            loading: false,
            error: message,
          });
          showMessage({
            type: 'danger',
            message,
          });
        } else {
          const message =
            'An internal server error occurred. Please try again later.';
          setFetchResult({
            ...fetchResult,
            loading: false,
            error: message,
          });
          showMessage({
            type: 'danger',
            message,
          });
        }
      });
  };

  const handleFetchSearchFilms = (page = 1) => {
    const searchedFilmTitle = form.values.title;

    if (searchedFilmTitle.length === 0) {
      showMessage({
        type: 'warning',
        message: 'You have to enter title of the film you wanted to search',
      });
      return;
    }

    setFetchResult({
      ...initialStates.fetchResult,
      loading: true,
    });

    fetchSearchFilms(searchedFilmTitle, page)
      .then(result => {
        setFetchResult({
          ...fetchResult,
          loading: false,
          data: result.data,
        });
      })
      .catch(error => {
        if (error.response && error.response.status === 400) {
          const {message} = error.response.data;
          setFetchResult({
            ...fetchResult,
            loading: false,
            error: message,
          });
          showMessage({
            type: 'danger',
            message,
          });
        } else {
          const message =
            'An internal server error occurred. Please try again later.';
          setFetchResult({
            ...fetchResult,
            loading: false,
            error: message,
          });
          showMessage({
            type: 'danger',
            message,
          });
        }
      });
  };

  const renderModalContent = useCallback(() => {
    const selectedSearchedFilm = fetchResult.data.results.find(
      searchedFilm => searchedFilm.id === modalState.selectedSearchedFilmId,
    );

    return (
      <View>
        <Image
          style={styles.modal.bottomImage}
          source={{
            uri: `http://image.tmdb.org/t/p/w500/${selectedSearchedFilm.poster_path}`,
          }}
        />

        <View style={styles.modal.content.container}>
          <View style={styles.modal.content.closeButton.container}>
            <IconButton
              icon="close"
              iconColor={MD2Colors.black}
              size={20}
              onPress={toggleModal}
            />
          </View>

          <View style={styles.modal.content.backdropImage.container}>
            <Avatar.Image
              size={100}
              source={{
                uri: `${
                  selectedSearchedFilm.backdrop_path
                    ? `http://image.tmdb.org/t/p/w500/${selectedSearchedFilm.backdrop_path}`
                    : `http://image.tmdb.org/t/p/w500/${selectedSearchedFilm.poster_path}`
                }`,
              }}
            />
          </View>

          <Text variant="titleLarge" style={styles.modal.content.title}>
            {selectedSearchedFilm.title}
          </Text>

          {/* TODO: Modal , butunuyle ScrollView icerisinde kaplanir. Boylece film hakkinda onyazinin tamami okunmus olur */}
          <Text variant="bodyMedium" style={styles.modal.content.overview}>
            {selectedSearchedFilm.overview.length > 300
              ? selectedSearchedFilm.overview.slice(0, 300).concat('...')
              : selectedSearchedFilm.overview}
          </Text>

          <Text variant="bodyMedium" style={styles.modal.content.releaseDate}>
            Released at {selectedSearchedFilm.release_Date}
          </Text>

          <View style={styles.modal.content.buttons.container}>
            <TouchableOpacity
              style={styles.modal.content.buttons.button.container(
                SavedFilmStatus.Watched,
              )}
              onPress={() => handleFetchSaveFilm(SavedFilmStatus.Watched)}>
              <Text
                variant="bodyMedium"
                style={styles.modal.content.buttons.button.text}>
                Set this film as watched
              </Text>
            </TouchableOpacity>
            <TouchableOpacity
              style={styles.modal.content.buttons.button.container(
                SavedFilmStatus.NotWatched,
              )}
              onPress={() => handleFetchSaveFilm(SavedFilmStatus.NotWatched)}>
              <Text
                variant="bodyMedium"
                style={styles.modal.content.buttons.button.text}>
                Set this film as not watched
              </Text>
            </TouchableOpacity>
          </View>
        </View>
      </View>
    );
  }, [modalState.selectedSearchedFilmId]);

  return (
    <View style={styles.container}>
      <Portal>
        <Modal
          visible={modalState.visible}
          onDismiss={toggleModal}
          contentContainerStyle={styles.modal.container}>
          {modalState.selectedSearchedFilmId && renderModalContent()}
        </Modal>
      </Portal>

      <Text variant="titleLarge" style={styles.header}>
        Search Films
      </Text>

      <TextInput
        label={
          form.isFocused !== null && (form.isFocused.title || form.values.title)
            ? ''
            : 'Enter title of film'
        }
        mode="flat"
        style={styles.input}
        activeUnderlineColor={MD3Colors.secondary20}
        value={form.values && form.values.title}
        onChangeText={text => handleSetValueOfFormKey('title', text)}
        onFocus={() => {
          handleFocus('title');
        }}
        onBlur={() => {
          handleBlur('title');
        }}
      />

      <Button
        mode="outlined"
        onPress={() => handleFetchSearchFilms()}
        loading={fetchResult.loading}>
        Search
      </Button>

      {fetchResult.data && (
        <ScrollView>
          <Text variant="titleMedium" style={styles.table.title}>
            Searched Films
          </Text>

          <DataTable>
            <DataTable.Header>
              <DataTable.Title>Name</DataTable.Title>
              <DataTable.Title numeric>Released Date</DataTable.Title>
              <DataTable.Title numeric>Details</DataTable.Title>
            </DataTable.Header>
            {fetchResult.data.results.map(searchedFilm => {
              return (
                <DataTable.Row key={searchedFilm.id}>
                  <DataTable.Cell>{searchedFilm.title}</DataTable.Cell>
                  <DataTable.Cell numeric>
                    {searchedFilm.release_Date}
                  </DataTable.Cell>
                  <DataTable.Cell numeric>
                    <IconButton
                      icon="magnify"
                      iconColor={MD2Colors.black}
                      size={20}
                      onPress={() => {
                        setModalState({
                          visible: !modalState.visible,
                          selectedSearchedFilmId: searchedFilm.id,
                        });
                      }}
                    />
                  </DataTable.Cell>
                </DataTable.Row>
              );
            })}

            <DataTable.Pagination
              page={fetchResult.data.page}
              numberOfPages={fetchResult.data.total_Pages}
              onPageChange={page => handleFetchSearchFilms(page)}
              label={`${fetchResult.data.page} of ${fetchResult.data.total_Pages}`}
              showFastPaginationControls
            />
          </DataTable>
        </ScrollView>
      )}
    </View>
  );
}

export default SearchFilm;
