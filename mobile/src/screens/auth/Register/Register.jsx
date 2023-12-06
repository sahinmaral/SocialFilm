import {useState} from 'react';
import {Text, View, TextInput, TouchableOpacity} from 'react-native';
import {Dropdown} from 'react-native-element-dropdown';
import baseStyles from '../../../styles/baseStyles';
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
          <Text style={baseStyles.auth.form.label}>Email</Text>
          <TextInput
            placeholder={
              form.isFocused.email || form.values.email
                ? ''
                : 'Enter your email'
            }
            style={baseStyles.auth.form.input}
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

        <View style={baseStyles.auth.form.group}>
          <Text style={baseStyles.auth.form.label}>Username</Text>
          <TextInput
            placeholder={
              form.isFocused.username || form.values.username
                ? ''
                : 'Enter your username'
            }
            style={baseStyles.auth.form.input}
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
            keyboardType="visible-password"
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

        <TouchableOpacity
          style={baseStyles.auth.form.submitButton.container}
          onPress={handleSubmit}>
          <Text style={baseStyles.auth.form.submitButton.text}>
            Continue Register
          </Text>
        </TouchableOpacity>

        <View style={baseStyles.auth.form.signUp.container}>
          <Text>Do you have an account? </Text>
          <Text
            style={baseStyles.auth.form.signUp.link}
            onPress={() => navigation.navigate('Login')}>
            Log in
          </Text>
        </View>
      </View>
    </View>
  );
}

export default Register;
