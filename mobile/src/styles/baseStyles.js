import {StyleSheet} from 'react-native';

const baseStyles = StyleSheet.create({
  auth: {
    container: {
      flex: 1,
      backgroundColor: 'lightblue',
    },
    header: {
      container: {flex: 0.6},
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
          color: '#f0f8ff',
        },
      },
      logo: {
        container: {
          height: 50,
          gap: 10,
          marginVertical: 50,
          alignItems: 'center',
          justifyContent: 'center',
        },
        text: {
          color: 'black',
          fontSize: 40,
          fontWeight: 'bold',
        },
      },
    },
    form: {
      container: {
        gap: 10,
        paddingHorizontal: 20,
        justifyContent: 'center',
        backgroundColor: 'white',
        borderTopRightRadius: 40,
        borderTopLeftRadius: 40,
      },
      group: {
        marginVertical: 5,
        gap: 10,
      },
      label: {
        fontWeight: '600',
        fontSize: 20,
      },
      input: {
        borderWidth: 1,
        borderColor: 'lightgray',
        fontSize: 15,
        marginVertical: 0,
        paddingVertical: 5,
      },
      forgotPassword: {
        color: '#267FF3',
        fontWeight: '500',
        textAlign: 'right',
      },
      submitButton: {
        container: {
          backgroundColor: '#267FF3',
          borderRadius: 5,
          paddingVertical: 10,
          alignItems: 'center',
        },
        text: {
          fontSize: 15,
          color: 'white',
        },
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
  },
});

export default baseStyles;
