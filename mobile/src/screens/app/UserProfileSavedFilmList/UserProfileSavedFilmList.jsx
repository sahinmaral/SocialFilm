import {fetchGetSavedFilmsOfUser} from '../../../services/APIService';
import {useState,useCallback} from 'react';
import {useFocusEffect} from '@react-navigation/native';
import { TextInput,View,FlatList } from 'react-native';

function UserProfileSavedFilmList({route}) {
  const {userId} = route.params;

  const initialStates = {
    fetchResult: {
      loading: false,
      error: null,
      data: null,
    },
  };

  const {handleBlur, handleFocus, form, handleSetValueOfFormKey} =
    useCustomForm(['title']);

  const [fetchResult, setFetchResult] = useState(initialStates.fetchResult);

  const handleFetchSavedFilmsOfUser = useCallback((page = 1) => {
    fetchGetSavedFilmsOfUser(userId, page)
      .then(result => {
        setFetchResult({
          ...fetchResult,
          data: result.data,
          loading: false,
        });
      })
      .catch(error => {
        setFetchResult({
          ...fetchResult,
          error,
          loading: false,
        });
      });
  });

  useFocusEffect(handleFetchSavedFilmsOfUser());

  const Item = ({title}) => (
    <View style={styles.item}>
      <Text style={styles.title}>{title}</Text>
    </View>
  );

  return (
    <View>
      <TextInput
        placeholder={
          form.isFocused !== null && (form.isFocused.title || form.values.title)
            ? ''
            : "Search user's watched films"
        }
        value={form.values && form.values.title}
        onChangeText={text => handleSetValueOfFormKey('title', text)}
        onFocus={() => {
          handleFocus('title');
        }}
        onBlur={() => {
          handleBlur('title');
        }}
      />

      <FlatList
        data={fetchResult.data}
        renderItem={({item: savedFilm}) => <Item savedFilm={savedFilm} />}
        keyExtractor={item => item.id}
      />
    </View>
  );
}

export default UserProfileSavedFilmList;
