import {StyleSheet} from 'react-native';
import {MD2Colors} from 'react-native-paper';

const styles = StyleSheet.create({
  container: {
    padding: 10,
    backgroundColor: MD2Colors.white,
    flex: 1,
    justifyContent: 'center',
  },
  header: {textAlign: 'center'},
  modal: {
    container: {
      backgroundColor: MD2Colors.white,
      margin: 10,
      borderRadius: 10,
    },
    bottomImage: {height: 500, width: '100%', opacity: 0.5, borderRadius: 10},
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
        container: {alignItems: 'flex-end'},
      },
      backdropImage: {
        container: {alignItems: 'center'},
      },
      title: {textAlign: 'center', color: MD2Colors.black, fontWeight: '700'},
      overview: {textAlign: 'center', color: MD2Colors.black},
      releaseDate: {textAlign: 'center', color: MD2Colors.black},
      buttons: {
        container: {
          gap: 10,
          marginTop: 20,
          flex: 1,
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
            color: MD2Colors.white,
            fontWeight: 'bold',
            textAlign: 'center',
          },
        },
      },
    },
  },
  input: {
    backgroundColor: 'white',
    paddingHorizontal: 0,
    height: 25,
    marginVertical: 20,
  },
  table: {
    title: {textAlign: 'center', marginTop: 20},
  },
});

export default styles;
