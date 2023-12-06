import {useCallback, useMemo, useState} from 'react';
import {
  Image,
  Modal,
  TextInput,
  TouchableOpacity,
  View,
  Text,
  Button,
  FlatList,
} from 'react-native';
import {showMessage} from 'react-native-flash-message';
import {useSelector} from 'react-redux';
import {fetchSaveFilm, fetchSearchFilms} from '../../../services/APIService';
import styles from './SearchFilm.styles';
import SavedFilmStatus from '../../../enums/SavedFilmStatus';
import useCustomForm from '../../../hooks/useCustomForm';
import {default as FeatherIcon} from 'react-native-vector-icons/Feather';

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
    useCustomForm(['name']);

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
    fetchSaveFilm(modalState.selectedSearchedFilmId, user.userId, status)
      .then(response => {
        clearFetchState();
        clearModalState();

        return response;
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
      })
      .then(response => {
        showMessage({
          type: 'success',
          message: response.data.message,
        });
      });
  };

  const handleFetchSearchFilms = (page = 1) => {
    const searchedFilmTitle = form.values.name;

    if (searchedFilmTitle.length === 0) {
      showMessage({
        type: 'warning',
        message: 'You have to enter name of the film you wanted to search',
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

  const TableItem = ({savedFilm}) => {
    return (
      <TouchableOpacity
        onPress={() => {
          setModalState({
            selectedSearchedFilmId: savedFilm.id,
            visible: !modalState.visible,
          });
        }}
        style={{
          alignItems: 'center',
          flexDirection: 'row',
          justifyContent: 'space-between',
          marginVertical: 5,
        }}>
        <Image
          style={{
            height: 50,
            width: 50,
            borderRadius: 25,
            resizeMode: 'center',
          }}
          source={{
            uri: `http://image.tmdb.org/t/p/w500/${savedFilm.posterPath}`,
          }}
        />
        <Text>{savedFilm.name}</Text>
      </TouchableOpacity>
    );
  };

  const tableItemsData = useMemo(() => {
    return fetchResult.data?.data;
  }, [fetchResult.data]);

  const renderModalContent = useCallback(() => {
    if (!fetchResult.data || !modalState.selectedSearchedFilmId) {
      return null;
    }

    const selectedSearchedFilm = fetchResult.data.data.find(
      searchedFilm => searchedFilm.id === modalState.selectedSearchedFilmId,
    );

    return (
      <View>
        <Image
          style={styles.modal.bottomImage}
          source={{
            uri: `http://image.tmdb.org/t/p/w500/${selectedSearchedFilm.posterPath}`,
          }}
        />

        <View style={styles.modal.content.container}>
          <View style={{flex: 1}}>
            <View style={styles.modal.content.closeButton.container}>
              <TouchableOpacity onPress={toggleModal}>
                <FeatherIcon name="x" color={'black'} size={24} />
              </TouchableOpacity>
            </View>

            <View style={{flex: 0.7, gap: 15}}>
              <View style={styles.modal.content.backdropImage.container}>
                <Image
                  style={{
                    height: 100,
                    aspectRatio: 1.5,
                    resizeMode: 'contain',
                    width: '100%',
                    borderRadius: 25,
                  }}
                  source={{
                    uri: `${
                      selectedSearchedFilm.backdrop_path
                        ? `http://image.tmdb.org/t/p/w500/${selectedSearchedFilm.backdropPath}`
                        : `http://image.tmdb.org/t/p/w500/${selectedSearchedFilm.posterPath}`
                    }`,
                  }}
                />
              </View>

              <Text style={styles.modal.content.title}>
                {selectedSearchedFilm.name}
              </Text>

              {/* TODO: Modal , butunuyle ScrollView icerisinde kaplanir. Boylece film hakkinda onyazinin tamami okunmus olur */}
              <Text style={styles.modal.content.overview}>
                {selectedSearchedFilm.overview.length > 300
                  ? selectedSearchedFilm.overview.slice(0, 300).concat('...')
                  : selectedSearchedFilm.overview}
              </Text>

              <Text style={styles.modal.content.releaseDate}>
                Released at {selectedSearchedFilm.releaseYear}
              </Text>
            </View>

            <View style={styles.modal.content.buttons.container}>
              <TouchableOpacity
                style={styles.modal.content.buttons.button.container(
                  SavedFilmStatus.Watched,
                )}
                onPress={() => handleFetchSaveFilm(SavedFilmStatus.Watched)}>
                <Text style={styles.modal.content.buttons.button.text}>
                  Set this film as watched
                </Text>
              </TouchableOpacity>
              <TouchableOpacity
                style={styles.modal.content.buttons.button.container(
                  SavedFilmStatus.NotWatched,
                )}
                onPress={() => handleFetchSaveFilm(SavedFilmStatus.NotWatched)}>
                <Text style={styles.modal.content.buttons.button.text}>
                  Set this film as not watched
                </Text>
              </TouchableOpacity>
            </View>
          </View>
        </View>
      </View>
    );
  }, [fetchResult.data, modalState.selectedSearchedFilmId]);

  return (
    <View style={styles.container}>
      <Modal
        animationType="slide"
        transparent={true}
        visible={modalState.visible}
        onRequestClose={toggleModal}>
        <View style={styles.modal.container}>{renderModalContent()}</View>
      </Modal>

      <View style={{flex: 0.2}}>
        <TextInput
          placeholder={
            form.isFocused !== null && (form.isFocused.name || form.values.name)
              ? ''
              : 'Enter name of film'
          }
          style={styles.input}
          value={form.values && form.values.name}
          onChangeText={text => handleSetValueOfFormKey('name', text)}
          onFocus={() => {
            handleFocus('name');
          }}
          onBlur={() => {
            handleBlur('name');
          }}
        />

        <TouchableOpacity>
          <Button
            onPress={() => handleFetchSearchFilms()}
            title="Search"></Button>
        </TouchableOpacity>
      </View>

      <View style={{flex: 0.8}}>
        {fetchResult.data && (
          <View>
            <Text style={styles.table.title}>Searched Films</Text>

            <FlatList
              data={tableItemsData}
              renderItem={({item: savedFilm}) => (
                <TableItem savedFilm={savedFilm} />
              )}
            />
          </View>
        )}
      </View>
    </View>
  );
}

export default SearchFilm;
