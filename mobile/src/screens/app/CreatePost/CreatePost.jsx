import {useCallback, useState} from 'react';
import {
  TouchableOpacity,
  View,
  TextInput,
  FlatList,
  Platform,
} from 'react-native';
import {
  ActivityIndicator,
  Button,
  Icon,
  MD2Colors,
  Text,
} from 'react-native-paper';
import {launchImageLibrary} from 'react-native-image-picker';
import {
  fetchCreatePost,
  fetchGetSavedFilmsOfUser,
} from '../../../services/APIService';
import {useSelector} from 'react-redux';
import {showMessage} from 'react-native-flash-message';
import SavedFilmStatus from '../../../enums/SavedFilmStatus';
import styles from './CreatePost.styles';
import {useFocusEffect} from '@react-navigation/native';

function CreatePost() {
  const initialStates = {
    form: {
      values: {
        content: '',
        selectedImages: [],
        selectedFilm: null,
      },
      isFocused: {
        content: false,
      },
    },
    fetchSavedFilmResult: {
      error: null,
      loading: true,
      data: null,
      searchSavedFilm: '',
    },
  };

  const [form, setForm] = useState(initialStates.form);

  const {user} = useSelector(state => state.auth);

  const [postResult, setPostResult] = useState({
    loading: false,
    error: null,
  });

  const [fetchSavedFilmResult, setFetchSavedFilmResult] = useState(
    initialStates.fetchSavedFilmResult,
  );

  const handleFetchGetSavedFilmsOfUser = () => {
    fetchGetSavedFilmsOfUser(
      user.userId,
      fetchSavedFilmResult.data
        ? fetchSavedFilmResult.data.metaData.currentPage + 1
        : 1,
      10,
      SavedFilmStatus.Watched,
      fetchSavedFilmResult.searchSavedFilm,
    )
      .then(res => {
        if (!form.values.selectedFilm) {
          setFetchSavedFilmResult({
            ...fetchSavedFilmResult,
            data: {
              data: [...res.data.data],
              metaData: res.data.metaData,
            },
          });
        } else {
          setFetchSavedFilmResult(prevState => ({
            ...prevState,
            data: {
              data: prevState.data
                ? [...prevState.data.data, ...res.data.data]
                : [...res.data.data],
              metaData: res.data.metaData,
            },
          }));
        }
      })
      .catch(() => {
        const message =
          'An internal server error occurred. Please try again later.';

        setFetchSavedFilmResult(prevState => ({
          ...prevState,
          error: message,
        }));
      })
      .finally(() => {
        setFetchSavedFilmResult(prevState => ({
          ...prevState,
          loading: false,
        }));
      });
  };

  useFocusEffect(
    useCallback(() => {
      handleFetchGetSavedFilmsOfUser();
    }, [fetchSavedFilmResult.searchSavedFilm]),
  );

  const handleFocus = formKey => {
    Object.keys(form.isFocused).forEach(key => {
      if (key === formKey) {
        setForm({
          ...form,
          isFocused: {...form.isFocused, [formKey]: true},
        });
      } else {
        setForm({
          ...form,
          isFocused: {...form.isFocused, [formKey]: true},
        });
      }
    });
  };

  const handleBlur = formKey => {
    Object.keys(form.isFocused).forEach(key => {
      if (key === formKey) {
        setForm({
          ...form,
          isFocused: {...form.isFocused, [formKey]: false},
        });
      } else {
        setForm({
          ...form,
          isFocused: {...form.isFocused, [formKey]: false},
        });
      }
    });
  };

  const openImageLibrary = () => {
    let options = {
      storageOptions: {
        path: 'image',
      },
    };

    launchImageLibrary(options, response => {
      if (response.didCancel) {
        console.log('User cancelled image picker');
      } else if (response.error) {
        console.log('Image picker error: ', response.error);
      } else {
        console.log(response);

        let imageUri = response.uri || response.assets?.[0]?.uri;
        let imageType = response.type || response.assets?.[0]?.type;
        let imageFileName = response.fileName || response.assets?.[0]?.fileName;
        let imageName = response.fileName || response.assets?.[0]?.fileName;

        setForm({
          ...form,
          values: {
            ...form.values,
            selectedImages: [
              ...form.values.selectedImages,
              {
                uri: imageUri,
                type: imageType,
                fileName: imageFileName,
                name: imageName,
              },
            ],
          },
        });
      }
    });
  };

  const removeSelectedImage = selectedImage => {
    setForm({
      ...form,
      values: {
        ...form.values,
        selectedImages: form.values.selectedImages.filter(
          selectedImageItem => selectedImageItem !== selectedImage,
        ),
      },
    });
  };

  const SelectedImageItem = ({selectedImage}) => {
    return (
      <View
        style={{
          flexDirection: 'row',
          margin: 5,
          justifyContent: 'space-between',
        }}>
        <Text variant="bodyMedium" style={{flex: 6 / 10}}>
          {selectedImage.name}
        </Text>
        <TouchableOpacity
          onPress={() => removeSelectedImage(selectedImage)}
          style={{
            borderWidth: 1,
            borderColor: MD2Colors.black,
            padding: 5,
            justifyContent: 'center',
            alignSelf: 'center',
            alignItems: 'center',
            flex: 1 / 10,
          }}>
          <Icon source="minus" color={MD2Colors.black} size={20} />
        </TouchableOpacity>
      </View>
    );
  };

  const handleSubmit = () => {
    setPostResult({
      ...postResult,
      loading: true,
    });

    const formData = new FormData();
    formData.append('userId', user.userId);
    formData.append('filmId', form.values.selectedFilm.id);
    formData.append('content', form.values.content);

    form.values.selectedImages.forEach(image => {
      formData.append('files', {
        name: image.fileName,
        type: image.type,
        uri:
          Platform.OS === 'ios' ? image.uri.replace('file://', '') : image.uri,
      });
    });

    fetchCreatePost(formData)
      .then(res => {
        const {message} = res.data;

        setPostResult({
          ...postResult,
          loading: false,
        });

        setForm(initialStates.form);

        showMessage({
          type: 'success',
          message,
        });
      })
      .catch(error => {
        if (error.response && error.response.status === 400) {
          const {message} = error.response.data;
          setPostResult({
            ...postResult,
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
          setPostResult({
            ...postResult,
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

  const renderSavedFilmDropdownItem = ({item}) => (
    <TouchableOpacity
      onPress={() =>
        setForm({...form, values: {...form.values, selectedFilm: item.film}})
      }>
      <View style={styles.filmToPost.dropdown.row.container}>
        <Text>{item.film.name}</Text>

        {form.values.selectedFilm &&
          form.values.selectedFilm.id === item.film.id && (
            <Icon source="check" color={'green'} size={20} />
          )}
      </View>
    </TouchableOpacity>
  );

  return (
    <View style={styles.container}>
      <View style={styles.content.container}>
        <Text variant="bodyMedium">Content</Text>
        <TextInput
          multiline={true}
          label={
            form.isFocused.content || form.values.content
              ? ''
              : 'Enter content of post'
          }
          activeUnderlineColor={'#ADADAD'}
          onChangeText={text =>
            setForm({...form, values: {...form.values, content: text}})
          }
          onFocus={() => {
            handleFocus('content');
          }}
          onBlur={() => {
            handleBlur('content');
          }}
          style={styles.content.input}
        />
      </View>

      <View style={styles.images.container}>
        <Text variant="bodyMedium">Images</Text>

        <View style={styles.images.imageUpload.container}>
          <TouchableOpacity
            onPress={openImageLibrary}
            style={styles.images.imageUpload.addRow}>
            <Icon source="plus" color={MD2Colors.black} size={20} />
          </TouchableOpacity>
          <Text variant="bodyMedium">
            Post photo count ({form.values.selectedImages.length})
          </Text>
        </View>

        <FlatList
          data={form.values.selectedImages}
          renderItem={({item: selectedImage}) => (
            <SelectedImageItem selectedImage={selectedImage} />
          )}
        />
      </View>

      <View style={styles.filmToPost.container}>
        <TextInput
          style={styles.filmToPost.input}
          placeholder="Search your saved films to post ..."
          value={fetchSavedFilmResult.searchSavedFilm}
          onChangeText={text => {
            setFetchSavedFilmResult({
              ...initialStates.fetchSavedFilmResult,
              searchSavedFilm: text,
            });
          }}
        />
        {fetchSavedFilmResult.loading ? (
          <ActivityIndicator />
        ) : (
          <FlatList
            data={fetchSavedFilmResult.data.data}
            keyExtractor={item => item.film.id}
            onEndReachedThreshold={0.5}
            onEndReached={() => {
              if (
                fetchSavedFilmResult.data.metaData.totalPages !==
                fetchSavedFilmResult.data.metaData.currentPage
              ) {
                handleFetchGetSavedFilmsOfUser();
              }
            }}
            renderItem={renderSavedFilmDropdownItem}
          />
        )}
      </View>

      <View style={styles.submitButton.container}>
        <Button
          mode="contained"
          onPress={handleSubmit}
          loading={postResult.loading}>
          Create Post
        </Button>
      </View>
    </View>
  );
}

export default CreatePost;
