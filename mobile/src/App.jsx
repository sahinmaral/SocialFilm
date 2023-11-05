import SavedFilmList from './screens/app/SavedFilmList';
import ContinueRegister from './screens/auth/ContinueRegister';
import Login from './screens/auth/Login';
import Register from './screens/auth/Register';
import {createNativeStackNavigator} from '@react-navigation/native-stack';
import {createBottomTabNavigator} from '@react-navigation/bottom-tabs';
import {default as FeatherIcon} from 'react-native-vector-icons/Feather';
import {MD2Colors} from 'react-native-paper';
import SearchFilm from './screens/app/SearchFilm';
import {View} from 'react-native';
import FlashMessage from 'react-native-flash-message';
import UserProfile from './screens/app/UserProfile';

const Stack = createNativeStackNavigator();
const Tab = createBottomTabNavigator();

const tabBarIconOptions = route => {
  let iconName;

  switch (route.name) {
    case 'SavedFilmList':
      iconName = 'list';
      break;
    case 'SearchFilm':
      iconName = 'search';
      break;
    case 'UserProfile':
      iconName = 'user';
      break;
    default:
      break;
  }

  return <FeatherIcon name={iconName} color={MD2Colors.black} size={24} />;
};

function AppTabNavigatorRoutes() {
  return (
    <Tab.Navigator
      screenOptions={({route}) => ({
        headerShown: false,
        tabBarShowLabel: false,
        tabBarIcon: () => tabBarIconOptions(route),
      })}>
      <Tab.Screen name="SearchFilm" component={SearchFilm} />
      <Tab.Screen name="SavedFilmList" component={SavedFilmList} />
      <Tab.Screen name="UserProfile" component={UserProfile} />
    </Tab.Navigator>
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
