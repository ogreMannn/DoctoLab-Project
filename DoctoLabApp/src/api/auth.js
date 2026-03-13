import apiClient from './client';
import AsyncStorage from '@react-native-async-storage/async-storage';

// Регистрация
export const registerUser = async (userData) => {
  try {
    // userData должен содержать то, что ждет бэк (например: email, password, name)
    const response = await apiClient.post('/auth/register', userData);
    return response.data; 
  } catch (error) {
    throw error.response?.data || error.message;
  }
};

// Логин
export const loginUser = async (email, password) => {
  try {
    const response = await apiClient.post('/auth/login', { email, password });
    
    // Бэкенд должен вернуть токен, сохраняем его в память телефона
    if (response.data && response.data.token) {
      await AsyncStorage.setItem('userToken', response.data.token);
    }
    
    return response.data;
  } catch (error) {
    throw error.response?.data || error.message;
  }
};

// Выход из аккаунта
export const logoutUser = async () => {
  await AsyncStorage.removeItem('userToken');
};