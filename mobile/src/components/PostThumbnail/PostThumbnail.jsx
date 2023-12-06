import styles from './PostThumbnail.styles';
import {Image, TouchableOpacity, View} from 'react-native';
import {default as MaterialCommunityIcons} from 'react-native-vector-icons/MaterialCommunityIcons';

const PostThumbnail = ({post, navigation}) => {
  return (
    <TouchableOpacity
      disabled={post.isCensored}
      style={styles.container}
      onPress={() => navigation.navigate('PostDetail', {postId: post.id})}>
      {post.isCensored && (
        <View
          style={{
            backgroundColor: 'rgba(211, 211, 211, 0.2)',
            height: 130,
            opacity: 1,
            justifyContent: 'center',
            alignItems: 'center',
          }}>
          <MaterialCommunityIcons name="eye-off" color={'#B5B4B4'} size={40} />
        </View>
      )}
      {!post.isCensored && (
        <View>
          <Image
            source={{
              uri: `https://res.cloudinary.com/sahinmaral/${post.photos[0].photoPath}`,
            }}
            style={styles.thumbnail}
          />
          <View style={styles.multipleImageIcon}>
            {post.photos.length > 1 && (
              <MaterialCommunityIcons
                name="image-multiple"
                color={'white'}
                size={24}
              />
            )}
          </View>
        </View>
      )}
    </TouchableOpacity>
  );
};

export default PostThumbnail;
