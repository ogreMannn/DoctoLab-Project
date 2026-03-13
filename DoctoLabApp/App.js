import React from 'react';
import { NavigationContainer } from '@react-navigation/native';
import { createNativeStackNavigator } from '@react-navigation/native-stack';

// Screens
import LoginScreen from './src/screens/LoginScreen';
import RegisterScreen from './src/screens/RegisterScreen';
import HomeScreen from './src/screens/HomeScreen';
import DoctorListScreen from './src/screens/DoctorListScreen';
import DoctorDetailScreen from './src/screens/DoctorDetailScreen';
import DoctorScheduleScreen from './src/screens/DoctorSheduleScreen';
import BookingScreen from './src/screens/BookingScreen';
import AppointmentsScreen from './src/screens/AppointmentsScreen';
import HospitalsScreen from './src/screens/HospitalsScreen';

const Stack = createNativeStackNavigator();

export default function App() {
  return (
    <NavigationContainer>
      <Stack.Navigator initialRouteName="Login">

        <Stack.Screen 
          name="Login" 
          component={LoginScreen} 
          options={{ headerShown: false }} 
        />

        <Stack.Screen 
          name="Register" 
          component={RegisterScreen} 
          options={{ title: 'Регистрация' }} 
        />

        <Stack.Screen 
          name="Home" 
          component={HomeScreen} 
          options={{ headerShown: false }} 
        />

        <Stack.Screen 
          name="Doctors" 
          component={DoctorListScreen} 
          options={{ title: 'Doctors' }} 
        />

        <Stack.Screen 
          name="DoctorDetail" 
          component={DoctorDetailScreen} 
        />

        <Stack.Screen 
          name="DoctorSchedule" 
          component={DoctorScheduleScreen} 
        />

        <Stack.Screen 
          name="Booking" 
          component={BookingScreen} 
        />

        <Stack.Screen 
          name="Appointments" 
          component={AppointmentsScreen} 
        />

        <Stack.Screen
          name="Hospitals"
          component={HospitalsScreen}
          options={{ title: 'Госпитали' }}
        />

      </Stack.Navigator>
    </NavigationContainer>
  );
}