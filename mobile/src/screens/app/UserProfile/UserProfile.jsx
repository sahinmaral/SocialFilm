import React, {useEffect, useState} from 'react';
import {FlatList, Image, StyleSheet, View} from 'react-native';
import {showMessage} from 'react-native-flash-message';
import {
  ActivityIndicator,
  Avatar,
  MD2Colors,
  MD3Colors,
  Text,
} from 'react-native-paper';
import {useSelector} from 'react-redux';
import {
  fetchGetUserInformations,
  fetchPostsByUserId,
} from '../../../services/APIService';

import PostThumbnail from '../../../components/PostThumbnail';
import styles from './UserProfile.styles';

function UserProfile({navigation}) {
  const initialStates = {
    fetchResult: {
      loading: true,
      error: null,
      data: null,
    },
    pageNumberOfPosts: 1,
  };

  const {user} = useSelector(state => state.auth);

  const [fetchResult, setFetchResult] = useState(initialStates.fetchResult);
  const [pageNumberOfPosts, setPageNumberOfPosts] = useState(
    initialStates.pageNumberOfPosts,
  );

  useEffect(() => {
    const getUserInfoPromise = fetchGetUserInformations(user.userId);
    const getPostsPromise = fetchPostsByUserId(user.userId, pageNumberOfPosts);

    Promise.all([getUserInfoPromise, getPostsPromise])
      .then(([userInfoResponse, postsResponse]) => {
        setFetchResult({
          ...fetchResult,
          loading: false,
          data: {
            userInformations: userInfoResponse.data,
            posts: postsResponse.data,
          },
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
  }, [user]);

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
                25
              </Text>
              <Text variant="bodyMedium">Posts</Text>
            </View>
            <View style={styles.statistics.row.container}>
              <Text variant="bodyMedium" style={styles.statistics.row.values}>
                25
              </Text>
              <Text variant="bodyMedium">Watched Films</Text>
            </View>
            <View style={styles.statistics.row.container}>
              <Text variant="bodyMedium" style={styles.statistics.row.values}>
                25
              </Text>
              <Text variant="bodyMedium">Friends</Text>
            </View>
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
