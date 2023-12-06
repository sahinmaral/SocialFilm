import {useEffect, useState} from 'react';
import {
  FlatList,
  View,
  Text,
  Image,
  TouchableOpacity,
  ActivityIndicator,
} from 'react-native';
import {showMessage} from 'react-native-flash-message';
import {useSelector} from 'react-redux';
import {
  fetchGetOtherUserPosts,
  fetchGetUserInformations,
  fetchGetUserProfileStatistics,
  fetchPostsByUserId,
} from '../../../services/APIService';
import PostThumbnail from '../../../components/PostThumbnail';
import styles from './UserProfile.styles';

function UserProfile({navigation, route}) {
  const initialStates = {
    fetchResult: {
      loading: true,
      error: null,
      data: null,
    },
  };

  const {user} = useSelector(state => state.auth);

  const [userId] = useState(
    route.params && route.params.userId ? route.params.userId : user.userId,
  );

  const [fetchResult, setFetchResult] = useState(initialStates.fetchResult);

  useEffect(() => {
    const getUserInfoPromise = fetchGetUserInformations(userId);
    const getPostsPromise =
      userId === user.userId
        ? fetchPostsByUserId(
            userId,
            !fetchResult.data || !fetchResult.data.postsResponse
              ? 1
              : fetchResult.data.postsResponse.metaData.currentPage + 1,
          )
        : fetchGetOtherUserPosts(
            user.userId,
            userId,
            !fetchResult.data || !fetchResult.data.postsResponse
              ? 1
              : fetchResult.data.postsResponse.metaData.currentPage + 1,
          );
    const getUserProfileStatisticsPromise =
      fetchGetUserProfileStatistics(userId);

    Promise.all([
      getUserInfoPromise,
      getPostsPromise,
      getUserProfileStatisticsPromise,
    ])
      .then(
        ([userInfoResponse, postsResponse, userProfileStatisticsResponse]) => {
          setFetchResult({
            ...fetchResult,
            loading: false,
            data: {
              userInformations: userInfoResponse.data,
              posts: postsResponse.data,
              userProfileStatistics: userProfileStatisticsResponse.data,
            },
          });
        },
      )
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
  }, [userId]);

  if (fetchResult.loading) {
    return (
      <View style={styles.loading}>
        <ActivityIndicator size={40} color={'darkred'} />
      </View>
    );
  }

  return (
    <View style={styles.container}>
      <View style={styles.informations.container}>
        <View style={styles.username.container}>
          <Text variant="bodyLarge" style={styles.username.value}>
            {fetchResult.data.userInformations.userName}
          </Text>
        </View>
        <View style={styles.statistics.container}>
          <Image
            style={{height: 80, width: 80, borderRadius: 40}}
            source={{
              uri: fetchResult.data.userInformations.profilePhotoURL
                ? `https://res.cloudinary.com/sahinmaral/${fetchResult.data.userInformations.profilePhotoURL}`
                : 'https://res.cloudinary.com/sahinmaral/image/upload/v1699171684/profileImages/default.png',
            }}
          />
          <View style={styles.statistics.description.container}>
            <View style={styles.statistics.row.container}>
              <Text style={styles.statistics.row.values}>
                {fetchResult.data.userProfileStatistics.postCount}
              </Text>
              <Text>Posts</Text>
            </View>
            <TouchableOpacity
              onPress={() =>
                navigation.navigate('SavedFilmList', {userId: user.userId})
              }>
              <View style={styles.statistics.row.container}>
                <Text style={styles.statistics.row.values}>
                  {fetchResult.data.userProfileStatistics.watchedFilmCount}
                </Text>
                <Text>Watched Films</Text>
              </View>
            </TouchableOpacity>
            <TouchableOpacity
              onPress={() =>
                navigation.navigate('UserProfileFriends', {userId: user.userId})
              }>
              <View style={styles.statistics.row.container}>
                <Text style={styles.statistics.row.values}>
                  {fetchResult.data.userProfileStatistics.friendCount}
                </Text>
                <Text>Friends</Text>
              </View>
            </TouchableOpacity>
          </View>
        </View>
      </View>
      <FlatList
        style={styles.posts.container}
        numColumns={3}
        data={fetchResult.data.posts.data}
        renderItem={({item}) => {
          return <PostThumbnail post={item} navigation={navigation} />;
        }}
      />
    </View>
  );
}

export default UserProfile;
