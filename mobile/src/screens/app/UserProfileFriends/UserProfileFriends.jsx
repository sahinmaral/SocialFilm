import {
  ActivityIndicator,
  Avatar,
  MD2Colors,
  MD3Colors,
  Text,
  TextInput,
} from 'react-native-paper';
import {fetchGetUserFriendsById} from '../../../services/APIService';
import {useState} from 'react';
import {useCallback} from 'react';
import {useFocusEffect} from '@react-navigation/native';
import useCustomForm from '../../../hooks/useCustomForm';
import {View, FlatList} from 'react-native';
import {useSelector} from 'react-redux';
import UserFriendListItem from '../../../components/UserFriendListItem';

function UserProfileFriends({route, navigation}) {
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

  const {handleBlur, handleFocus, form, handleSetValueOfFormKey} =
    useCustomForm(['title']);

  const [fetchResult, setFetchResult] = useState(initialStates.fetchResult);

  const handleFetchGetUserFriendsById = (page = 1) => {
    fetchGetUserFriendsById(userId, page)
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

  useFocusEffect(
    useCallback(() => {
      handleFetchGetUserFriendsById();
    }, []),
  );

  if (fetchResult.loading) {
    return (
      <View style={{flex: 1, justifyContent: 'center', alignItems: 'center'}}>
        <ActivityIndicator
          animating={true}
          size={40}
          color={MD2Colors.red800}
        />
      </View>
    );
  }

  return (
    <View style={{padding: 10}}>
      <TextInput
        label={
          form.isFocused !== null && (form.isFocused.title || form.values.title)
            ? ''
            : "Search user's friend by username"
        }
        mode="flat"
        style={{
          backgroundColor: 'white',
          paddingVertical: 5,
          paddingHorizontal: 10,
          height: 25,
          marginVertical: 20,
        }}
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

      {fetchResult.data && (
        <FlatList
          data={fetchResult.data.data}
          renderItem={({item: userFriend}) => (
            <UserFriendListItem
              userFriend={userFriend}
            />
          )}
          keyExtractor={item => item.id}
        />
      )}
    </View>
  );
}

export default UserProfileFriends;
