import axios from 'axios';
import {useState} from 'react';
import {View, Text, TextInput} from 'react-native';
import {Dropdown} from 'react-native-element-dropdown';
import DateTimePicker from '@react-native-community/datetimepicker';
import baseStyles from '../../../styles/baseStyles';
import * as Progress from 'react-native-progress';
import styles from './ContinueRegister.styles';
import {TouchableOpacity} from 'react-native';

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
          <Text style={baseStyles.auth.form.label}>Name</Text>
          <TextInput
            placeholder={
              form.isFocused.firstName || form.values.firstName
                ? ''
                : 'Enter your name'
            }
            style={baseStyles.auth.form.input}
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

        <View style={baseStyles.auth.form.group}>
          <Text style={baseStyles.auth.form.label}>Middle Name</Text>
          <TextInput
            placeholder={
              form.isFocused.middleName || form.values.middleName
                ? ''
                : 'Enter your middle name (optional)'
            }
            style={baseStyles.auth.form.input}
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

        <View style={baseStyles.auth.form.group}>
          <Text style={baseStyles.auth.form.label}>Surname</Text>
          <TextInput
            placeholder={
              form.isFocused.lastName || form.values.lastName
                ? ''
                : 'Enter your surname'
            }
            style={baseStyles.auth.form.input}
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

        <View style={baseStyles.auth.form.group}>
          <Text style={baseStyles.auth.form.label}>Birth Date</Text>
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

          <View style={styles.form.birthDate.container}>
            <Text>{form.values.birthDate.toLocaleDateString()}</Text>
            <TouchableOpacity
              style={styles.form.birthDate.button}
              onPress={() =>
                setForm({
                  ...form,
                  isFocused: {
                    ...form.isFocused,
                    birthDate: true,
                  },
                })
              }>
              <Text>Toggle select birth date</Text>
            </TouchableOpacity>
          </View>
        </View>

        <TouchableOpacity
          style={baseStyles.auth.form.submitButton.container}
          onPress={handleSubmit}>
          <Text style={baseStyles.auth.form.submitButton.text}>
            Complete Register
          </Text>
        </TouchableOpacity>

        <View style={baseStyles.auth.form.signUp.container}>
          <Text>Do you want to go back? </Text>
          <Text
            style={baseStyles.auth.form.signUp.link}
            onPress={() => navigation.navigate('Register')}>
            Register
          </Text>
        </View>
      </View>

      <Progress.Bar
        progress={0.5}
        color={'#267FF3'}
        unfilledColor={'#267FF3'}
        width={200}
      />
    </View>
  );
}

export default ContinueRegister;
