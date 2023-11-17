import SavedFilmList from './screens/app/SavedFilmList';
import ContinueRegister from './screens/auth/ContinueRegister';
import Login from './screens/auth/Login';
import Register from './screens/auth/Register';
import {createNativeStackNavigator} from '@react-navigation/native-stack';
import {Text} from 'react-native-paper';
import SearchFilm from './screens/app/SearchFilm';
import FlashMessage from 'react-native-flash-message';
import UserProfile from './screens/app/UserProfile';
import CreatePost from './screens/app/CreatePost';
import PostDetail from './screens/app/PostDetail';
import {View} from 'react-native';
import TabButtonGroup from './components/TabButtonGroup';
import CommentsDetail from './screens/app/CommentsDetail';
import UserProfileSavedFilmList from './screens/app/UserProfileSavedFilmList/UserProfileSavedFilmList';
import UserProfileFriends from './screens/app/UserProfileFriends/UserProfileFriends';

const Stack = createNativeStackNavigator();

function Homepage() {
  return (
    <View>
      <Text>Homepage</Text>
    </View>
  );
}

function AppTabNavigatorRoutes() {
  return (
    <View style={{flex: 1}}>
      <Stack.Navigator initialRouteName="Homepage">
        <Stack.Screen name="Homepage" component={Homepage} />
        <Stack.Screen
          name="SearchFilm"
          component={SearchFilm}
          options={{title: 'Search Films'}}
        />
        <Stack.Screen
          name="SavedFilmList"
          component={SavedFilmList}
          options={{title: 'Saved Films'}}
        />
        <Stack.Screen name="CreatePost" component={CreatePost} />
        <Stack.Screen
          name="PostDetail"
          component={PostDetail}
          options={{title: 'Posts'}}
        />
        <Stack.Screen
          name="CommentsDetail"
          component={CommentsDetail}
          options={{
            title: 'Comments',
          }}
        />
        <Stack.Screen
          name="UserProfileSavedFilmList"
          component={UserProfileSavedFilmList}
        />
        <Stack.Screen
          name="UserProfile"
          component={UserProfile}
          options={{
            headerShown: false,
          }}
        />
          <Stack.Screen
          name="UserProfileFriends"
          component={UserProfileFriends}
          options={{
            title: 'Friends',
          }}
        />
      </Stack.Navigator>
      <TabButtonGroup />
    </View>
  );
}

function App() {
  return (
    <View style={{flex: 1}}>
      <Stack.Navigator
        initialRouteName="Login"
        screenOptions={{headerShown: false}}>
        <Stack.Screen name="Login" component={Login} />
        <Stack.Screen name="Register" component={Register} />
        <Stack.Screen name="ContinueRegister" component={ContinueRegister} />
        <Stack.Screen name="App" component={AppTabNavigatorRoutes} />
      </Stack.Navigator>
      <FlashMessage position="top" />
    </View>
  );
}

export default App;
