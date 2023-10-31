import axios from 'axios';
import React, {useState} from 'react';
import {View} from 'react-native';
import {Dropdown} from 'react-native-element-dropdown';
import {Text, TextInput, Button} from 'react-native-paper';
import styles from './Register.styles';

const data = [
  {label: 'English', value: 'EN'},
  {label: 'Türkçe', value: 'TR'},
];

function Register({navigation}) {
  const [dropdownValue, setDropdownValue] = useState('EN');
  const [form, setForm] = useState({
    values: {
      email: 'mustafa.maral@hotmail.com',
      username: 'mustafamaral',
      password: 'Abc1234.',
    },
    isFocused: {
      email: false,
      username: false,
      password: false,
    },
    isPasswordShown: true,
  });

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
    navigation.navigate('ContinueRegister', form.values);
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
            Email
          </Text>
          <TextInput
            label={
              form.isFocused.email || form.values.email
                ? ''
                : 'Enter your email'
            }
            mode="flat"
            style={styles.form.input}
            activeUnderlineColor={'#ADADAD'}
            value={form.values.email}
            onChangeText={text =>
              setForm({...form, values: {...form.values, email: text}})
            }
            onFocus={() => {
              handleFocus('email');
            }}
            onBlur={() => {
              handleBlur('email');
            }}
          />
        </View>

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
        </View>

        <Button
          mode="contained"
          style={styles.form.submitButton}
          onPress={handleSubmit}>
          Continue Register
        </Button>

        <View style={styles.form.signUp.container}>
          <Text variant="bodyMedium">Do you have an account? </Text>
          <Text
            variant="bodyMedium"
            style={styles.form.signUp.link}
            onPress={() => navigation.navigate('Login')}>
            Log in
          </Text>
        </View>
      </View>
    </View>
  );
}

export default Register;
