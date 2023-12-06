import Login from './screens/auth/Login';
import ContinueRegister from './screens/auth/ContinueRegister';
import Register from './screens/auth/Register';
import {createNativeStackNavigator} from '@react-navigation/native-stack';
import FlashMessage from 'react-native-flash-message';
import SearchFilm from './screens/app/SearchFilm';
import Homepage from './screens/app/Homepage';
import TabButtonGroup from './components/TabButtonGroup';
import SavedFilmList from './screens/app/SavedFilmList';
import CreatePost from './screens/app/CreatePost';
import UserProfile from './screens/app/UserProfile';
import PostDetail from './screens/app/PostDetail';
import CommentsDetail from './screens/app/CommentsDetail';
import UserProfileSavedFilmList from './screens/app/UserProfileSavedFilmList';
import UserProfileFriends from './screens/app/UserProfileFriends';
import {View} from 'react-native';

const Stack = createNativeStackNavigator();

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
          name="UserProfile"
          component={UserProfile}
          options={{
            headerShown: false,
          }}
        />
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
