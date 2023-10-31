import {StyleSheet} from 'react-native';
import {MD3Colors} from 'react-native-paper';

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: 'lightblue',
  },
  language: {
    container: {
      height: 50,
      alignItems: 'center',
      justifyContent: 'center',
    },
    dropdown: {
      width: 100,
    },
    placeholderStyle: {
      fontSize: 16,
      color: MD3Colors.secondary70,
    },
  },
  logo: {
    container: {
      height: 50,
      gap:10,
      marginVertical: 50,
      alignItems: 'center',
      justifyContent: 'center',
    },
    text: {
      fontWeight: 'bold',
    },
  },
  form: {
    container: {
      gap: 10,
      flex: 0.9,
      paddingHorizontal: 20,
      justifyContent: 'center',
      backgroundColor: 'white',
      borderTopRightRadius: 40,
      borderTopLeftRadius: 40,
    },
    group: {
      marginVertical: 15,
      gap: 10,
    },
    label: {
      fontWeight: '600',
      fontSize: 20,
    },
    input: {
      backgroundColor: 'white',
      paddingHorizontal: 0,
      height: 25,
    },
    forgotPassword: {
      color: '#267FF3',
      fontWeight: '500',
      textAlign: 'right',
    },
    submitButton: {
      backgroundColor: '#267FF3',
      borderRadius: 5,
      marginTop: 0,
    },
    signUp: {
      container: {
        marginTop: 0,
        justifyContent: 'center',
        flexDirection: 'row',
      },
      link: {
        color: '#267FF3',
      },
    },
  },
});

export default styles;
