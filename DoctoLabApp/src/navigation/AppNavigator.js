import React from "react";
import { NavigationContainer } from "@react-navigation/native";
import { createNativeStackNavigator } from "@react-navigation/native-stack";

import LoginScreen from "../screens/LoginScreen";
import RegisterScreen from "../screens/RegisterScreen";
import HomeScreen from "../screens/HomeScreen";
import DoctorListScreen from "../screens/DoctorListScreen";
import DoctorDetailScreen from "../screens/DoctorDetailScreen";
import DoctorScheduleScreen from "../screens/DoctorScheduleScreen";
import BookingScreen from "../screens/BookingScreen";
import AppointmentsScreen from "../screens/AppointmentsScreen";
import HospitalsScreen from "../screens/HospitalsScreen";
const Stack = createNativeStackNavigator();

export default function AppNavigator() {
  return (
    <NavigationContainer>
      <Stack.Navigator initialRouteName="Login">

        <Stack.Screen name="Login" component={LoginScreen} />
        <Stack.Screen name="Register" component={RegisterScreen} />
        <Stack.Screen name="Home" component={HomeScreen} />
        <Stack.Screen name="Doctors" component={DoctorListScreen} />
        <Stack.Screen name="DoctorDetail" component={DoctorDetailScreen} />
        <Stack.Screen name="DoctorSchedule" component={DoctorScheduleScreen} />
        <Stack.Screen name="Booking" component={BookingScreen} />
        <Stack.Screen name="Appointments" component={AppointmentsScreen} />
        <Stack.Screen name="Hospitals" component={HospitalsScreen} options={{ title: 'Госпитали' }} />

      </Stack.Navigator>
    </NavigationContainer>
  );
}