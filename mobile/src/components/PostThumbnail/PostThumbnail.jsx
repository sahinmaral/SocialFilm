import {MD2Colors} from 'react-native-paper';
import styles from './PostThumbnail.styles';
import {Image, TouchableOpacity, View} from 'react-native';
import {default as MaterialCommunityIcons} from 'react-native-vector-icons/MaterialCommunityIcons';

const PostThumbnail = ({post, navigation}) => {
  return (
    <TouchableOpacity
      style={styles.container}
      onPress={() => navigation.navigate('PostDetail', {postId: post.id})}>
      <Image
        source={{
          uri: `https://res.cloudinary.com/sahinmaral/${post.photos[1].photoPath}`,
        }}
        style={styles.thumbnail}
      />
      <View style={styles.multipleImageIcon}>
        {post.photos.length > 0 && (
          <MaterialCommunityIcons
            name="image-multiple"
            color={MD2Colors.white}
            size={24}
          />
        )}
      </View>
    </TouchableOpacity>
  );
};

export default PostThumbnail;
