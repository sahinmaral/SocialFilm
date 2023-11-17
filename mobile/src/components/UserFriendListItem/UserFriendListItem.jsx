import {View} from 'react-native';
import {Avatar, Text} from 'react-native-paper';
import styles from './UserFriendListItem.styles';
import {TouchableOpacity} from 'react-native';
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
        <Avatar.Image
          size={24}
          source={{
            uri: userFriend.profilePhotoURL
              ? `https://res.cloudinary.com/sahinmaral/${userFriend.profilePhotoURL}`
              : 'https://res.cloudinary.com/sahinmaral/image/upload/v1699171684/profileImages/default.png',
          }}
        />
        <Text variant="bodyMedium">{userFriend.userName}</Text>
      </View>
    </TouchableOpacity>
  );
}

export default UserFriendListItem;
