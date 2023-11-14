import {StyleSheet} from 'react-native';
import {MD2Colors} from 'react-native-paper';

const styles = StyleSheet.create({
  container: {padding: 10, flex: 1, gap: 10},
  content: {
    container: {flex: 1 / 10},
    input: {backgroundColor: 'white', padding: 5},
  },
  images: {
    container: {flex: 4 / 10, gap: 10},
    imageUpload: {
      container: {
        flexDirection: 'row',
        justifyContent: 'space-between',
        alignItems: 'center',
      },
      addRow: {
        borderWidth: 1,
        borderColor: MD2Colors.black,
        padding: 5,
        alignSelf: 'flex-start',
      },
    },
  },
  filmToPost: {
    container: {flex: 4 / 10, gap: 10},
    input: {borderWidth: 1, borderColor: MD2Colors.black},
    dropdown: {
      row: {
        container: {borderWidth: 1, borderColor: 'lightgray', padding: 5},
      },
    },
  },
  submitButton: {
    container: {flex: 1 / 10, justifyContent: 'center'},
  },
});

export default styles;
