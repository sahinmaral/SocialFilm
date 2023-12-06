import {useState} from 'react';
import {View, TextInput, Text, TouchableOpacity} from 'react-native';
import {Dropdown} from 'react-native-element-dropdown';
import {setUser} from '../../../redux/reducers/authSlice';
import {useDispatch} from 'react-redux';
import {showMessage} from 'react-native-flash-message';
import {fetchLogin} from '../../../services/APIService';
import baseStyles from '../../../styles/baseStyles';
import styles from './Login.styles';

const data = [
  {label: 'English', value: 'EN'},
  {label: 'Türkçe', value: 'TR'},
];

function Login({navigation}) {
  const [dropdownValue, setDropdownValue] = useState('EN');
  const [form, setForm] = useState({
    values: {
      username: 'sahinmaral',
      password: 'Abc1234.',
    },
    isFocused: {
      username: false,
      password: false,
    },
    isPasswordShown: true,
  });

  const dispatch = useDispatch();

  const handleFocus = formKey => {
    Object.keys(form.isFocused).forEach(key => {
      if (key === formKey) {
        setForm({
          ...form,
          isFocused: {...form.isFocused, [formKey]: true},
        });
      } else {
        setForm({
          ...form,
          isFocused: {...form.isFocused, [formKey]: true},
        });
      }
    });
  };

  const handleBlur = formKey => {
    Object.keys(form.isFocused).forEach(key => {
      if (key === formKey) {
        setForm({
          ...form,
          isFocused: {...form.isFocused, [formKey]: false},
        });
      } else {
        setForm({
          ...form,
          isFocused: {...form.isFocused, [formKey]: false},
        });
      }
    });
  };

  const handleSubmit = () => {
    fetchLogin(form.values.username, form.values.password)
      .then(result => {
        dispatch(setUser(result.data));
        navigation.navigate('App');
      })
      .catch(error => {
        if (error.response && error.response.status === 400) {
          const {message} = error.response.data;
          showMessage({
            type: 'danger',
            message,
          });
        } else {
          const message =
            'An internal server error occurred. Please try again later.';
          showMessage({
            type: 'danger',
            message,
          });
        }
      });
  };

  return (
    <View style={baseStyles.auth.container}>
      <View style={baseStyles.auth.header.container}>
        <View style={baseStyles.auth.header.language.container}>
          <Dropdown
            style={baseStyles.auth.header.language.dropdown}
            data={data}
            value={dropdownValue}
            labelField="label"
            valueField="value"
            placeholder={data[dropdownValue]}
            onChange={item => {
              setDropdownValue(item.value);
            }}
            placeholderStyle={baseStyles.auth.header.language.placeholderStyle}
          />
        </View>

        <View style={baseStyles.auth.header.logo.container}>
          <Text style={baseStyles.auth.header.logo.text}>SocialFilm</Text>
        </View>
      </View>

      <View style={[baseStyles.auth.form.container, styles.form.container]}>
        <View style={baseStyles.auth.form.group}>
          <Text style={baseStyles.auth.form.label}>Username</Text>
          <TextInput
            style={baseStyles.auth.form.input}
            onChangeText={text =>
              setForm({...form, values: {...form.values, username: text}})
            }
            onFocus={() => {
              handleFocus('username');
            }}
            onBlur={() => {
              handleBlur('username');
            }}
            value={form.values.username}
            placeholder={
              form.isFocused.username || form.values.username
                ? ''
                : 'Enter your username'
            }
            keyboardType="default"
          />
        </View>

        <View style={baseStyles.auth.form.group}>
          <Text style={baseStyles.auth.form.label}>Password</Text>
          <TextInput
            placeholder={
              form.isFocused.password || form.values.username
                ? ''
                : 'Enter your password'
            }
            style={baseStyles.auth.form.input}
            value={form.values.password}
            onChangeText={text =>
              setForm({...form, values: {...form.values, password: text}})
            }
            onFocus={() => {
              handleFocus('password');
            }}
            onBlur={() => {
              handleBlur('password');
            }}
            keyboardType="visible-password"
          />
          <Text style={baseStyles.auth.form.forgotPassword}>
            Forgot password ?
          </Text>
        </View>

        <TouchableOpacity
          style={baseStyles.auth.form.submitButton.container}
          onPress={handleSubmit}>
          <Text style={baseStyles.auth.form.submitButton.text}>Log in</Text>
        </TouchableOpacity>

        <View style={baseStyles.auth.form.signUp.container}>
          <Text>Don't have an account? </Text>
          <Text
            style={baseStyles.auth.form.signUp.link}
            onPress={() => navigation.navigate('Register')}>
            Sign up
          </Text>
        </View>
      </View>
    </View>
  );
}

export default Login;
