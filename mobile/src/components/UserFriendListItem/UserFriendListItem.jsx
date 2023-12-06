import {View, TouchableOpacity, Text, Image} from 'react-native';
import styles from './UserFriendListItem.styles';
import {useNavigation} from '@react-navigation/native';

function UserFriendListItem({userFriend}) {
  const navigation = useNavigation();

  return (
    <TouchableOpacity
      onPress={() =>
        navigation.navigate({
          name: 'UserProfile',
          params: {
            userId: userFriend.id,
          },
          key: `UserProfile-${userFriend.id}`,
        })
      }>
      <View style={styles.container}>
        <Image
          style={{height: 24, width: 24, borderRadius: 12}}
          source={{
            uri: userFriend.profilePhotoURL
              ? `https://res.cloudinary.com/sahinmaral/${userFriend.profilePhotoURL}`
              : 'https://res.cloudinary.com/sahinmaral/image/upload/v1699171684/profileImages/default.png',
          }}
        />
        <Text>{userFriend.userName}</Text>
      </View>
    </TouchableOpacity>
  );
}

export default UserFriendListItem;
