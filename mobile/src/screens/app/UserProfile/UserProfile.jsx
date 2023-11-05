import React, {useCallback, useEffect, useState} from 'react';
import {Image, ScrollView, StyleSheet, View} from 'react-native';
import {showMessage} from 'react-native-flash-message';
import {
  ActivityIndicator,
  Avatar,
  MD2Colors,
  MD3Colors,
  Text,
} from 'react-native-paper';
import {useSelector} from 'react-redux';
import {fetchGetUserInformations} from '../../../services/APIService';
import {default as MaterialCommunityIcons} from 'react-native-vector-icons/MaterialCommunityIcons';

function UserProfile() {
  const initialStates = {
    fetchResult: {
      loading: true,
      error: null,
      data: null,
    },
    allPosts: [],
  };

  const {user} = useSelector(state => state.auth);

  const [fetchResult, setFetchResult] = useState(initialStates.fetchResult);

  const handleFetchGetUserInformations = () => {
    fetchGetUserInformations(user.userId)
      .then(response => {
        setFetchResult({
          ...fetchResult,
          loading: false,
          data: response.data,
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

  const divideAllPostsByThree = () => {
    const items = [...Array(10).keys()];

    const chunkSize = Math.ceil(items.length / 4);

    const dividedArray = [];

    for (let i = 0; i < items.length; i += chunkSize) {
      const chunk = items.slice(i, i + chunkSize);
      dividedArray.push(chunk);
    }

    return dividedArray;
  };

  const renderPostsAsGrid = useCallback(() => {
    const dividedPosts = divideAllPostsByThree();

    return dividedPosts.map((postsOfRow,index) => {
      return (
        <View style={{flexDirection: 'row', gap: 2}} key={index}>
          {postsOfRow.map((post,index2) => {
            return (
              <View style={{flex: 1 / 3, height: 150}} key={index2}>
                <Image
                  source={{
                    uri: 'https://res.cloudinary.com/sahinmaral/image/upload/v1699171684/profileImages/default.png',
                  }}
                  style={{height: '100%', width: '100%', resizeMode: 'contain'}}
                />
              </View>
            );
          })}
        </View>
      );
    });
  },[divideAllPostsByThree])

  useEffect(() => {
    handleFetchGetUserInformations();
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
      <View style={styles.username.container}>
        <Text variant="bodyLarge" style={styles.username.value}>
          {fetchResult.data.userName}
        </Text>
      </View>
      <View style={styles.statistics.container}>
        <Avatar.Image
          size={80}
          source={{
            uri: fetchResult.data.profilePhotoURL
              ? `https://res.cloudinary.com/sahinmaral/${fetchResult.data.profilePhotoURL}`
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
      <ScrollView style={styles.posts.container}>
        {renderPostsAsGrid().map(postOfRow => {
          return postOfRow;
        })}
      </ScrollView>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    padding: 10,
    backgroundColor: MD2Colors.white,
    justifyContent: 'center',
  },
  loading: {flex: 1, justifyContent: 'center'},
  username: {
    container: {alignItems: 'center'},
    value: {fontWeight: 'bold'},
  },
  statistics: {
    container: {
      flexDirection: 'row',
      alignItems: 'center',
      justifyContent: 'space-evenly',
    },
    thumbnail: {
      height: 80,
      width: 80,
      borderRadius: 40,
      borderWidth: 1,
      borderColor: MD3Colors.secondary90,
    },
    description: {
      container: {flexDirection: 'row', gap: 10},
    },
    row: {
      container: {alignItems: 'center'},
      values: {fontWeight: 'bold'},
    },
  },
  posts: {
    container: {
      marginVertical: 10,
    },
  },
});

export default UserProfile;
