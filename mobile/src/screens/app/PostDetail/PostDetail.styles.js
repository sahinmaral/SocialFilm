import {StyleSheet} from 'react-native';

const styles = StyleSheet.create({
  container: {
    main:{padding: 10},
    content:{
      gap:10,
      flexGrow: 1
    }
  },
  user: {
    container: {flexDirection: 'row', alignContent: 'center', gap: 10},
    username: {fontWeight: 'bold'},
  },
  thumbnail: {
    container: {position: 'relative'},
    arrows: {
      container: {
        width: '100%',
        top: '50%',
        flexDirection: 'row',
        position: 'absolute',
        zIndex: 10,
      },
    },
    image: {
      height: 400,
      resizeMode: 'stretch',
      width: '100%',
    },
  },
  navigators: {
    container: {flexDirection: 'row', gap: 10, alignItems: 'center'},
    buttonGroup: {flexDirection: 'row', gap: 10, flex: 1},
    paginationButtonGroup: {
      container: {
        flex: 1,
        gap: 10,
        justifyContent: 'flex-start',
        flexDirection: 'row',
      },
      button: {
        borderWidth:1,
        width: 10,
        height: 10,
        borderRadius: 5,
      },
    },
  },
  aboutFilm: {
    container:{flexDirection: 'row'},
    name: {fontWeight: 'bold'}
  }
});

export default styles;
