import {useState, useEffect, useMemo, useRef} from 'react';
import {
  Image,
  TouchableOpacity,
  View,
  TextInput,
  Button,
  Text,
  ScrollView,
  ActivityIndicator,
} from 'react-native';
import {fetchGetPostById} from '../../../services/APIService';
import {showMessage} from 'react-native-flash-message';
import {default as FeatherIcon} from 'react-native-vector-icons/Feather';
import {default as MaterialCommunityIcon} from 'react-native-vector-icons/MaterialCommunityIcons';
import styles from './PostDetail.styles';

function PostDetail({navigation, route}) {
  const {postId} = route.params;

  const initialStates = {
    fetchResult: {
      loading: true,
      error: null,
      data: null,
    },
    postStates: {
      photos: {
        currentIndex: 0,
      },
    },
    commentStates: {
      value: '',
      visibility: false,
    },
  };

  const scrollViewRef = useRef(null);

  const [fetchResult, setFetchResult] = useState(initialStates.fetchResult);
  const [postStates, setPostStates] = useState(initialStates.postStates);
  const [commentStates, setCommentStates] = useState(
    initialStates.commentStates,
  );

  const handleFetchGetPostById = postId => {
    fetchGetPostById(postId)
      .then(res => {
        setFetchResult({
          ...fetchResult,
          data: res.data,
          loading: false,
        });
      })
      .catch(error => {
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
      });
  };

  const setPhotoIndexOfPostStates = index => {
    setPostStates({
      ...postStates,
      photos: {currentIndex: index},
    });
  };

  const toggleToShowTypeComment = () => {
    setCommentStates({...commentStates, visibility: !commentStates.visibility});
  };

  const scrollToEnd = () => {
    if (scrollViewRef.current) {
      scrollViewRef.current.scrollToEnd({animated: true});
    }
  };

  const reducedContentOfPost = useMemo(() => {
    return fetchResult.data
      ? fetchResult.data.content.length > 110
        ? fetchResult.data.content.slice(0, 110).concat('...')
        : fetchResult.data.content
      : null;
  }, [fetchResult]);

  const moveArrowVerticalPosition = useMemo(() => {
    if (postStates.photos.currentIndex === 0) {
      return 'flex-end';
    } else if (
      postStates.photos.currentIndex ===
      fetchResult.data.photos.length - 1
    ) {
      return 'flex-start';
    } else {
      return 'space-between';
    }
  }, [postStates.photos.currentIndex]);

  useEffect(() => {
    handleFetchGetPostById(postId);
  }, [postId]);

  if (fetchResult.loading) {
    return (
      <View style={{flex: 1}}>
        <ActivityIndicator size={40} color={'darkred'} />
      </View>
    );
  }

  return (
    <ScrollView
      style={styles.container.main}
      contentContainerStyle={styles.container.content}>
      <View style={styles.user.container}>
        <Image
          style={{height: 40, width: 40, borderRadius: 20}}
          source={{
            uri: 'https://res.cloudinary.com/sahinmaral/image/upload/v1699161684/profileImages/default.png',
          }}
        />
        <Text style={styles.user.username}>sahinnmarall</Text>
      </View>
      <View style={styles.thumbnail.container}>
        <View
          style={[
            styles.thumbnail.arrows.container,
            {justifyContent: moveArrowVerticalPosition},
          ]}>
          {postStates.photos.currentIndex !== 0 && (
            <TouchableOpacity
              onPress={() =>
                setPhotoIndexOfPostStates(postStates.photos.currentIndex - 1)
              }>
              <FeatherIcon name={'chevron-left'} color={'black'} size={24} />
            </TouchableOpacity>
          )}
          {postStates.photos.currentIndex !==
            fetchResult.data.photos.length - 1 && (
            <TouchableOpacity
              onPress={() =>
                setPhotoIndexOfPostStates(postStates.photos.currentIndex + 1)
              }>
              <FeatherIcon name={'chevron-right'} color={'black'} size={24} />
            </TouchableOpacity>
          )}
        </View>
        <Image
          source={{
            uri: `https://res.cloudinary.com/sahinmaral/${
              fetchResult.data.photos[postStates.photos.currentIndex].photoPath
            }`,
          }}
          style={styles.thumbnail.image}
        />
      </View>
      <View style={styles.navigators.container}>
        <View style={styles.navigators.buttonGroup}>
          <TouchableOpacity>
            <MaterialCommunityIcon
              name="heart-outline"
              size={24}
              color={'black'}
            />
          </TouchableOpacity>
        </View>
        <View style={styles.navigators.paginationButtonGroup.container}>
          {Array.from(Array(fetchResult.data.photos.length).keys()).map(
            (_, index) => {
              return (
                <TouchableOpacity
                  key={index}
                  onPress={() => setPhotoIndexOfPostStates(index)}>
                  <View
                    style={[
                      styles.navigators.paginationButtonGroup.button,
                      {
                        borderColor:
                          index === postStates.photos.currentIndex
                            ? 'blue'
                            : 'black',
                        backgroundColor:
                          index === postStates.photos.currentIndex
                            ? 'blue'
                            : 'white',
                      },
                    ]}
                  />
                </TouchableOpacity>
              );
            },
          )}
        </View>
      </View>
      <View style={styles.aboutFilm.container}>
        <Text>Film : </Text>
        <Text style={styles.aboutFilm.name}>
          {fetchResult.data.filmDetail.name}
        </Text>
      </View>
      <View>
        <Text>{reducedContentOfPost}</Text>
        <TouchableOpacity
          onPress={() => navigation.navigate('CommentsDetail', {postId})}>
          <Text style={{color: 'gray'}}>
            Show comments ({fetchResult.data.commentCount})
          </Text>
        </TouchableOpacity>
      </View>
      {commentStates.visibility && (
        <View style={{gap: 10}}>
          <TextInput
            onFocus={scrollToEnd}
            placeholder="Enter comment"
            multiline={true}
            style={{
              borderRadius: 5,
              borderWidth: 1,
              paddingVertical: 5,
              borderColor: 'lightgray',
            }}
          />
          <Button title={'Submit'} />
        </View>
      )}
    </ScrollView>
  );
}

export default PostDetail;
