import {StyleSheet} from 'react-native';

const styles = StyleSheet.create({
  form: {
    container: {
      flex: 1,
    },
    birthDate: {
      container: {
        flexDirection: 'row',
        alignItems:"center",
        justifyContent: 'space-between',
      },
      button: {
        borderColor: 'lightgray',
        borderWidth: 1,
        borderRadius: 10,
        padding: 5,
      },
    },
  },
});

export default styles;
