import axios from 'axios';
import React, {useState} from 'react';
import {View} from 'react-native';
import {Dropdown} from 'react-native-element-dropdown';
import {
  Text,
  TextInput,
  Button,
  ProgressBar,
  MD3Colors,
} from 'react-native-paper';
import styles from './ContinueRegister.styles';
import DateTimePicker from '@react-native-community/datetimepicker';

const data = [
  {label: 'English', value: 'EN'},
  {label: 'Türkçe', value: 'TR'},
];

function ContinueRegister({navigation, route}) {
  const {email, username, password} = route.params;

  const [dropdownValue, setDropdownValue] = useState('EN');
  const [form, setForm] = useState({
    values: {
      firstName: 'Mustafa',
      middleName: null,
      lastName: 'MARAL',
      birthDate: new Date(1967, 1, 1),
    },
    isFocused: {
      firstName: false,
      middleName: false,
      lastName: false,
      birthDate: false,
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
    axios
      .post('http://localhost:5133/api/Auth/register', {
        username,
        password,
        email,
        ...form.values,
      })
      .then(result => {
        if (result.status == 200) {
          navigation.navigate('Login');
        }
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
            Name
          </Text>
          <TextInput
            label={
              form.isFocused.firstName || form.values.firstName
                ? ''
                : 'Enter your name'
            }
            mode="flat"
            style={styles.form.input}
            activeUnderlineColor={'#ADADAD'}
            value={form.values.firstName}
            onChangeText={text =>
              setForm({...form, values: {...form.values, firstName: text}})
            }
            onFocus={() => {
              handleFocus('firstName');
            }}
            onBlur={() => {
              handleBlur('firstName');
            }}
          />
        </View>

        <View style={styles.form.group}>
          <Text variant="titleMedium" style={styles.form.label}>
            Middle Name
          </Text>
          <TextInput
            label={
              form.isFocused.middleName || form.values.middleName
                ? ''
                : 'Enter your middle name (optional)'
            }
            mode="flat"
            style={styles.form.input}
            activeUnderlineColor={'#ADADAD'}
            value={form.values.middleName}
            onChangeText={text =>
              setForm({...form, values: {...form.values, middleName: text}})
            }
            onFocus={() => {
              handleFocus('middleName');
            }}
            onBlur={() => {
              handleBlur('middleName');
            }}
          />
        </View>

        <View style={styles.form.group}>
          <Text variant="titleMedium" style={styles.form.label}>
            Surname
          </Text>
          <TextInput
            label={
              form.isFocused.lastName || form.values.lastName
                ? ''
                : 'Enter your surname'
            }
            mode="flat"
            style={styles.form.input}
            activeUnderlineColor={'#ADADAD'}
            value={form.values.lastName}
            onChangeText={text =>
              setForm({...form, values: {...form.values, lastName: text}})
            }
            onFocus={() => {
              handleFocus('lastName');
            }}
            onBlur={() => {
              handleBlur('lastName');
            }}
          />
        </View>

        <View style={styles.form.group}>
          <Text variant="titleMedium" style={styles.form.label}>
            Birth Date
          </Text>
          {form.isFocused.birthDate && (
            <DateTimePicker
              value={form.values.birthDate}
              is24Hour={true}
              mode="date"
              onChange={(e, selectedDate) =>
                setForm({
                  ...form,
                  isFocused: {
                    ...form.isFocused,
                    birthDate: false,
                  },
                  values: {
                    ...form.values,
                    birthDate: selectedDate,
                  },
                })
              }
            />
          )}

          <View style={styles.form.birthDate}>
            <Text>{form.values.birthDate.toLocaleDateString()}</Text>
            <Button
              mode="text"
              textColor="gray"
              onPress={() =>
                setForm({
                  ...form,
                  isFocused: {
                    ...form.isFocused,
                    birthDate: true,
                  },
                })
              }>
              Toggle select birth date
            </Button>
          </View>
        </View>

        <Button
          mode="contained"
          style={styles.form.submitButton}
          onPress={handleSubmit}>
          Complete Register
        </Button>

        <View style={styles.form.signUp.container}>
          <Text variant="bodyMedium">Do you want to go back? </Text>
          <Text
            variant="bodyMedium"
            style={styles.form.signUp.link}
            onPress={() => navigation.navigate('Register')}>
            Register
          </Text>
        </View>
      </View>

      <ProgressBar progress={0.5} color={'#267FF3'} />
    </View>
  );
}

export default ContinueRegister;
