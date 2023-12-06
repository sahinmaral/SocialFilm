import {useFocusEffect} from '@react-navigation/native';
import {useCallback, useState, useEffect, useMemo} from 'react';
import {
  Image,
  View,
  TouchableOpacity,
  FlatList,
  ActivityIndicator,
  Modal,
  Text,
} from 'react-native';
import {showMessage} from 'react-native-flash-message';
import {useSelector} from 'react-redux';
import SavedFilmStatus from '../../../enums/SavedFilmStatus';
import styles from './SavedFilmList.styles';
import {
  fetchGetSavedFilmsOfUser,
  fetchSaveFilm,
} from '../../../services/APIService';
import {default as FeatherIcon} from 'react-native-vector-icons/Feather';

function SavedFilmList({route}) {
  const {user} = useSelector(state => state.auth);

  const [userIdToSeeSavedFilmList, setUserIdToSeeSavedFilmList] =
    useState(null);

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

  const tableItemsData = useMemo(() => {
    return fetchResult.data?.data;
  }, [fetchResult.data]);

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
                    width: '100%',
                    aspectRatio: 1.5,
                    resizeMode: 'contain',
                    borderRadius: 25,
                  }}
                  source={{
                    uri: `${
                      selectedSavedFilm.film.backdrop_path
                        ? `http://image.tmdb.org/t/p/w500/${selectedSavedFilm.film.backdropPath}`
                        : `http://image.tmdb.org/t/p/w500/${selectedSavedFilm.film.posterPath}`
                    }`,
                  }}
                />
              </View>

              <Text style={styles.modal.content.title}>
                {selectedSavedFilm.film.name}
              </Text>

              {/* TODO: Modal , butunuyle ScrollView icerisinde kaplanir. Boylece film hakkinda onyazinin tamami okunmus olur */}
              <Text style={styles.modal.content.overview}>
                {selectedSavedFilm.film.overview.length > 300
                  ? selectedSavedFilm.film.overview.slice(0, 300).concat('...')
                  : selectedSavedFilm.film.overview}
              </Text>

              <Text style={styles.modal.content.releaseDate}>
                Released at {selectedSavedFilm.film.releaseYear}
              </Text>
            </View>

            <View style={styles.modal.content.buttons.container}>
              {selectedSavedFilm.status !== SavedFilmStatus.Watched && (
                <TouchableOpacity
                  style={styles.modal.content.buttons.button.container(
                    SavedFilmStatus.Watched,
                  )}
                  onPress={() => handleFetchSaveFilm(SavedFilmStatus.Watched)}>
                  <Text style={styles.modal.content.buttons.button.text}>
                    Set this film as watched
                  </Text>
                </TouchableOpacity>
              )}
            </View>
          </View>
        </View>
      </View>
    );
  };

  const handleFetchSavedFilmsOfUser = (page = 1) => {
    fetchGetSavedFilmsOfUser(userIdToSeeSavedFilmList, page)
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
        clearModalState();

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
    }, [userIdToSeeSavedFilmList]),
  );

  const TableItem = ({savedFilm}) => {
    return (
      <TouchableOpacity
        onPress={() => {
          setModalState({
            selectedSavedFilm: savedFilm,
            visible: !modalState.visible,
          });
        }}
        style={{
          flexDirection: 'row',
          justifyContent: 'space-between',
          marginVertical: 10,
          borderBottomWidth: 1,
          borderBottomColor: 'black',
        }}>
        <Text>{savedFilm.film.name}</Text>
        <Text style={styles.table.status(savedFilm.status).text}>
          {savedFilm.status === SavedFilmStatus.Watched
            ? 'Watched'
            : 'Not Watched'}
        </Text>
      </TouchableOpacity>
    );
  };

  useEffect(() => {
    const userIdExists = route.params && route.params.userId;
    if (userIdExists) {
      setUserIdToSeeSavedFilmList(userIdExists);
    } else {
      setUserIdToSeeSavedFilmList(user.userId);
    }
  }, []);

  if (fetchResult.loading) {
    return (
      <View style={{flex: 1, justifyContent: 'center', alignItems: 'center'}}>
        <ActivityIndicator size={40} color={'darkred'} />
      </View>
    );
  }

  return (
    <View style={styles.container}>
      <Modal
        animationType="slide"
        transparent={true}
        visible={modalState.visible}
        onRequestClose={toggleModal}>
        <View style={styles.modal.container}>
          {modalState.selectedSavedFilm && renderModalContent()}
        </View>
      </Modal>

      <FlatList
        data={tableItemsData}
        ListHeaderComponent={
          <View style={{flexDirection: 'row', justifyContent: 'space-between'}}>
            <Text style={{fontWeight: 700, color: 'black'}}>Name</Text>
            <Text style={{fontWeight: 700, color: 'black'}}>Status</Text>
          </View>
        }
        renderItem={({item: savedFilm}) => <TableItem savedFilm={savedFilm} />}
      />
    </View>
  );
}

export default SavedFilmList;
