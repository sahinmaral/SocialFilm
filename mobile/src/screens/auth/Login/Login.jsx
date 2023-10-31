import axios from 'axios';
import React, {useState} from 'react';
import {View} from 'react-native';
import {Dropdown} from 'react-native-element-dropdown';
import {Text, TextInput, Button} from 'react-native-paper';
import styles from './Login.styles';
import {setUser} from '../../../redux/reducers/authSlice';
import {useDispatch} from 'react-redux';

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
    axios
      .post('http://localhost:5133/api/Auth/login', {
        userName: form.values.username,
        password: form.values.password,
      })
      .then(result => {
        dispatch(setUser(result.data));
        navigation.navigate('App');
      })
      .catch(err => {
        console.log(err);
      });
  };

  return (
    <View style={styles.container}>
      <View style={{flex: 0.6}}>
        <View style={styles.language.container}>
          <Dropdown
            style={[styles.language.dropdown]}
            data={data}
            value={dropdownValue}
            labelField="label"
            valueField="value"
            placeholder={data[dropdownValue]}
            onChange={item => {
              setDropdownValue(item.value);
            }}
            placeholderStyle={styles.language.placeholderStyle}
          />
        </View>

        <View style={styles.logo.container}>
          <Text variant="headlineLarge" style={styles.logo.text}>
            SocialFilm
          </Text>
        </View>
      </View>

      <View style={styles.form.container}>
        <View style={styles.form.group}>
          <Text variant="titleMedium" style={styles.form.label}>
            Username
          </Text>
          <TextInput
            label={
              form.isFocused.username || form.values.username
                ? ''
                : 'Enter your username'
            }
            mode="flat"
            style={styles.form.input}
            activeUnderlineColor={'#ADADAD'}
            value={form.values.username}
            onChangeText={text =>
              setForm({...form, values: {...form.values, username: text}})
            }
            onFocus={() => {
              handleFocus('username');
            }}
            onBlur={() => {
              handleBlur('username');
            }}
          />
        </View>

        <View style={styles.form.group}>
          <Text variant="titleMedium" style={styles.form.label}>
            Password
          </Text>
          <TextInput
            label={
              form.isFocused.password || form.values.username
                ? ''
                : 'Enter your password'
            }
            mode="flat"
            style={styles.form.input}
            value={form.values.password}
            activeUnderlineColor={'#ADADAD'}
            secureTextEntry={form.isPasswordShown}
            right={
              <TextInput.Icon
                icon="eye"
                onPress={() =>
                  setForm({...form, isPasswordShown: !form.isPasswordShown})
                }
              />
            }
            onChangeText={text =>
              setForm({...form, values: {...form.values, password: text}})
            }
            onFocus={() => {
              handleFocus('password');
            }}
            onBlur={() => {
              handleBlur('password');
            }}
          />
          <Text variant="bodyMedium" style={styles.form.forgotPassword}>
            Forgot password ?
          </Text>
        </View>

        <Button
          mode="contained"
          style={styles.form.submitButton}
          onPress={handleSubmit}>
          Log in
        </Button>

        <View style={styles.form.signUp.container}>
          <Text variant="bodyMedium">Don't have an account? </Text>
          <Text
            variant="bodyMedium"
            style={styles.form.signUp.link}
            onPress={() => navigation.navigate('Register')}>
            Sign up
          </Text>
        </View>
      </View>
    </View>
  );
}

export default Login;
