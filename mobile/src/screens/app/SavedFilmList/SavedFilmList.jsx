import {useFocusEffect} from '@react-navigation/native';
import axios from 'axios';
import {useCallback, useEffect, useState} from 'react';
import {StyleSheet, Image, View, TouchableOpacity} from 'react-native';
import {showMessage} from 'react-native-flash-message';
import {
  Text,
  DataTable,
  IconButton,
  Badge,
  Portal,
  Modal,
  Avatar,
  Icon,
  MD2Colors,
  ActivityIndicator,
} from 'react-native-paper';
import {useSelector} from 'react-redux';
import SavedFilmStatus from '../../../enums/SavedFilmStatus';
import styles from './SavedFilmList.styles';
import {
  fetchGetSavedFilmsOfUser,
  fetchSaveFilm,
} from '../../../services/APIService';

function SavedFilmList() {
  const {user} = useSelector(state => state.auth);

  const initialStates = {
    modalState: {
      visible: false,
      selectedSavedFilm: null,
    },
    fetchResult: {
      loading: true,
      error: null,
      data: null,
    },
  };

  const [fetchResult, setFetchResult] = useState(initialStates.fetchResult);
  const [modalState, setModalState] = useState(initialStates.modalState);

  const toggleModal = () => {
    setModalState({...modalState, visible: !modalState.visible});
  };

  const clearModalState = () => {
    setModalState(initialStates.modalState);
  };

  const renderModalContent = () => {
    const {selectedSavedFilm} = {...modalState};

    return (
      <View>
        <Image
          style={styles.modal.bottomImage}
          source={{
            uri: `http://image.tmdb.org/t/p/w500/${selectedSavedFilm.film.posterPath}`,
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

          <View style={styles.modal.content.backdropImage}>
            <Avatar.Image
              size={100}
              source={{
                uri: `http://image.tmdb.org/t/p/w500/${selectedSavedFilm.film.backdropPath}`,
              }}
            />
          </View>

          <Text variant="titleLarge" style={styles.modal.content.title}>
            {selectedSavedFilm.film.name}
          </Text>

          <Text variant="bodyMedium" style={styles.modal.content.overview}>
            {selectedSavedFilm.film.overview.length > 300
              ? selectedSavedFilm.film.overview.slice(0, 300).concat('...')
              : selectedSavedFilm.film.overview}
          </Text>

          <Text variant="bodyMedium" style={styles.modal.content.releaseDate}>
            Released at {selectedSavedFilm.film.releaseYear}
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
  };

  const handleFetchSavedFilmsOfUser = (page = 1) => {
    fetchGetSavedFilmsOfUser(user.userId, page)
      .then(result => {
        setFetchResult({
          ...fetchResult,
          data: result.data,
          loading: false,
        });
      })
      .catch(error => {
        setFetchResult({
          ...fetchResult,
          error,
          loading: false,
        });
      });
  };

  const handleFetchSaveFilm = status => {

    fetchSaveFilm({
      filmId: modalState.selectedSavedFilm.film.id.toString(),
      userId: user.userId,
      status: status,
    })
      .then(result => {
        showMessage({
          message: result.data.message,
          type: 'success',
        });
        clearModalState();
      })
      .then(() => {
        handleFetchSaveFilm();
      })
      .catch(error => {
        clearModalState()

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

  useFocusEffect(
    useCallback(() => {
      handleFetchSavedFilmsOfUser();
    }, []),
  );

  return (
    <View style={styles.container}>
      <Text variant="titleLarge" style={styles.header}>
        Saved Films
      </Text>

      <Portal>
        <Modal
          visible={modalState.visible}
          onDismiss={toggleModal}
          contentContainerStyle={styles.modal.container}>
          {modalState.selectedSavedFilm && renderModalContent()}
        </Modal>
      </Portal>

      {fetchResult.loading && (
        <View style={styles.loading}>
          <ActivityIndicator
            animating={true}
            size={40}
            color={MD2Colors.red800}
          />
        </View>
      )}
      {!fetchResult.loading && (
        <DataTable>
          <DataTable.Header>
            <DataTable.Title>Name</DataTable.Title>
            <DataTable.Title numeric>Status</DataTable.Title>
            <DataTable.Title numeric>Details</DataTable.Title>
          </DataTable.Header>
          {fetchResult.data.datas.map(savedFilm => {
            return (
              <DataTable.Row key={savedFilm.id}>
                <DataTable.Cell>
                  <Text variant="bodyMedium">{savedFilm.film.name}</Text>
                </DataTable.Cell>
                <DataTable.Cell style={styles.table.status.container}>
                  <Badge
                    style={styles.table.status.badge(
                      savedFilm.status === SavedFilmStatus.Watched
                        ? 'green'
                        : 'darkred',
                    )}>
                    <Text variant="bodyMedium" style={styles.table.status.text}>
                      {savedFilm.status === SavedFilmStatus.Watched
                        ? 'Watched'
                        : 'Not Watched'}
                    </Text>
                  </Badge>
                </DataTable.Cell>
                <DataTable.Cell numeric>
                  <IconButton
                    icon="magnify"
                    iconColor={MD2Colors.black}
                    size={20}
                    onPress={() => {
                      setModalState({
                        selectedSavedFilm: savedFilm,
                        visible: !modalState.visible,
                      });
                    }}
                  />
                </DataTable.Cell>
              </DataTable.Row>
            );
          })}

          <DataTable.Pagination
            page={fetchResult.data.pageNumber}
            numberOfPages={fetchResult.data.totalPages}
            onPageChange={page => handleFetchSavedFilmsOfUser(page)}
            label={`${fetchResult.data.pageNumber} of ${fetchResult.data.totalPages}`}
            showFastPaginationControls
          />
        </DataTable>
      )}
    </View>
  );
}

export default SavedFilmList;
