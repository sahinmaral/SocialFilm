import {StyleSheet} from 'react-native';

const styles = StyleSheet.create({
  container: {
    padding: 10,
    backgroundColor: 'white',
    flex: 1,
    justifyContent: 'center',
  },
  header: {textAlign: 'center'},
  modal: {
    container: {
      flex: 0.95,
      backgroundColor: 'white',
      margin: 10,
      borderRadius: 10,
    },
    bottomImage: {
      height: '100%',
      width: '100%',
      opacity: 0.5,
      borderRadius: 10,
    },
    content: {
      container: {
        position: 'absolute',
        top: 0,
        left: 0,
        right: 0,
        bottom: 0,
        paddingHorizontal: 10,
        gap: 10,
      },
      closeButton: {
        container: {alignItems: 'flex-end', marginTop: 10, flex: 0.1},
      },
      backdropImage: {
        container: {alignItems: 'center'},
      },
      title: {textAlign: 'center', color: 'black', fontWeight: '700'},
      overview: {textAlign: 'center', color: 'black'},
      releaseDate: {textAlign: 'center', color: 'black'},
      buttons: {
        container: {
          gap: 10,
          marginTop: 20,
          flex: 0.2,
          flexDirection: 'row',
          alignItems: 'center',
        },
        button: {
          container: status => {
            return {
              height: 60,
              paddingVertical: 10,
              paddingHorizontal: 10,
              borderRadius: 10,
              flex: 1,
              backgroundColor: status === 1 ? 'darkgreen' : 'darkred',
            };
          },
          text: {
            color: 'white',
            fontWeight: 'bold',
            textAlign: 'center',
          },
        },
      },
    },
  },
  input: {
    borderWidth: 1,
    borderColor: 'lightgray',
    fontSize: 15,
    marginVertical: 5,
    paddingVertical: 5,
  },
  table: {
    title: {textAlign: 'center', marginTop: 20},
  },
});

export default styles;
