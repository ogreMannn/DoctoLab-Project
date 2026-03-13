import axios from 'axios';
import AsyncStorage from '@react-native-async-storage/async-storage';
import { Platform } from 'react-native';


export const BASE_URL =
  Platform.OS === 'android'
    ? 'https://xlv0gld2-5191.euw.devtunnels.ms/api'
    : 'https://xlv0gld2-5191.euw.devtunnels.ms/api';

const apiClient = axios.create({
  baseURL: BASE_URL,
  timeout: 30000,
});


apiClient.interceptors.request.use(
  async (config) => {
    // Достаем токен из памяти телефона
    const token = await AsyncStorage.getItem('userToken');
    if (token) {
      // Если токен есть, прикрепляем его к запросу
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

export default apiClient;