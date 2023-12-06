import {StyleSheet} from 'react-native';

const styles = StyleSheet.create({
  container: {
    flex: 1,
    padding: 10,
    backgroundColor: 'white',
    justifyContent: 'center',
  },
  loading: {flex: 1, justifyContent: 'center'},
  informations: {
    container: {flex: 1 / 3},
  },
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
      borderColor: 'lightgray',
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
    container: {flex: 2 / 3},
  },
});

export default styles;
