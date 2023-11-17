import React, {useEffect, useState} from 'react';
import {FlatList, View} from 'react-native';
import {showMessage} from 'react-native-flash-message';
import {ActivityIndicator, Avatar, MD2Colors, Text} from 'react-native-paper';
import {useSelector} from 'react-redux';
import {
  fetchGetOtherUserPosts,
  fetchGetUserInformations,
  fetchGetUserProfileStatistics,
  fetchPostsByUserId,
} from '../../../services/APIService';

import PostThumbnail from '../../../components/PostThumbnail';
import styles from './UserProfile.styles';
import {TouchableOpacity} from 'react-native';

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
        <ActivityIndicator
          animating={true}
          size={40}
          color={MD2Colors.red800}
        />
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
          <Avatar.Image
            size={80}
            source={{
              uri: fetchResult.data.userInformations.profilePhotoURL
                ? `https://res.cloudinary.com/sahinmaral/${fetchResult.data.userInformations.profilePhotoURL}`
                : 'https://res.cloudinary.com/sahinmaral/image/upload/v1699171684/profileImages/default.png',
            }}
          />
          <View style={styles.statistics.description.container}>
            <View style={styles.statistics.row.container}>
              <Text variant="bodyMedium" style={styles.statistics.row.values}>
                {fetchResult.data.userProfileStatistics.postCount}
              </Text>
              <Text variant="bodyMedium">Posts</Text>
            </View>
            <TouchableOpacity
              onPress={() =>
                navigation.navigate('SavedFilmList', {userId: user.userId})
              }>
              <View style={styles.statistics.row.container}>
                <Text variant="bodyMedium" style={styles.statistics.row.values}>
                  {fetchResult.data.userProfileStatistics.watchedFilmCount}
                </Text>
                <Text variant="bodyMedium">Watched Films</Text>
              </View>
            </TouchableOpacity>
            <TouchableOpacity
              onPress={() =>
                navigation.navigate('UserProfileFriends', {userId: user.userId})
              }>
              <View style={styles.statistics.row.container}>
                <Text variant="bodyMedium" style={styles.statistics.row.values}>
                  {fetchResult.data.userProfileStatistics.friendCount}
                </Text>
                <Text variant="bodyMedium">Friends</Text>
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
