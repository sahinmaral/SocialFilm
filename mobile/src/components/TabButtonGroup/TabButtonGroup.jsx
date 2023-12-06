import {useNavigation} from '@react-navigation/native';
import React from 'react';
import { TouchableOpacity, View } from 'react-native';
import {default as FeatherIcon} from 'react-native-vector-icons/Feather';
import styles from './TabButtonGroup.styles';

function TabButtonGroup() {
  const navigation = useNavigation();

  const handleTabPress = screenName => {
    navigation.navigate(screenName);
  };

  const renderTabBarIcons = route => {
    let iconName;

    switch (route) {
      case 'SavedFilmList':
        iconName = 'list';
        break;
      case 'SearchFilm':
        iconName = 'search';
        break;
      case 'UserProfile':
        iconName = 'user';
        break;
      case 'CreatePost':
        iconName = 'plus-square';
        break;
      default:
        break;
    }

    return <FeatherIcon name={iconName} color={"black"} size={24} />;
  };

  return (
    <View style={styles.container}>
      <TouchableOpacity
        onPress={() => handleTabPress('SearchFilm')}
        style={styles.button.container}>
        {renderTabBarIcons('SearchFilm')}
      </TouchableOpacity>
      <TouchableOpacity
        onPress={() => handleTabPress('SavedFilmList')}
        style={styles.button.container}>
        {renderTabBarIcons('SavedFilmList')}
      </TouchableOpacity>
      <TouchableOpacity
        onPress={() => handleTabPress('CreatePost')}
        style={styles.button.container}>
        {renderTabBarIcons('CreatePost')}
      </TouchableOpacity>
      <TouchableOpacity
        onPress={() => handleTabPress('UserProfile')}
        style={styles.button.container}>
        {renderTabBarIcons('UserProfile')}
      </TouchableOpacity>
    </View>
  );
}



export default TabButtonGroup;
