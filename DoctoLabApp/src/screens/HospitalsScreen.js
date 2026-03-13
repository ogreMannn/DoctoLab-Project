import React, { useState, useEffect } from 'react';
import { View, Text, StyleSheet, FlatList, ActivityIndicator, Alert } from 'react-native';
import apiClient, { BASE_URL } from '../api/client';

const HospitalScreen = ({ navigation }) => {
  const [hospitals, setHospitals] = useState([]);
  const [loading, setLoading] = useState(true);

  // useEffect запустит fetchHospitals один раз при открытии экрана
  useEffect(() => {
    fetchHospitals();
  }, []);

  const fetchHospitals = async () => {
    try {
      const response = await apiClient.get('/hospitals');
      // axios (apiClient) кладет данные в response.data
      setHospitals(response.data); 
    } catch (error) {
      console.error("Ошибка загрузки госпиталей:", error);

      const isNetworkError = !error.response;
      const errorText = isNetworkError
        ? `Network error при запросе к ${BASE_URL}`
        : `Ошибка ${error.response.status}: ${error.response.data?.message || error.message}`;

      Alert.alert(
        "Ошибка",
        `Не удалось загрузить список больниц.\n${errorText}`
      );
    } finally {
      setLoading(false); // Выключаем крутилку загрузки в любом случае
    }
  };

  // Как выглядит одна карточка больницы в списке
  const renderItem = ({ item }) => (
    <View style={styles.card}>
      <Text style={styles.name}>{item.name || 'Больница без названия'}</Text>
      <Text style={styles.address}>{item.address || 'Адрес не указан'}</Text>
    </View>
  );

  // Пока данные грузятся, показываем крутилку
  if (loading) {
    return (
      <View style={styles.centered}>
        <ActivityIndicator size="large" color="#0066cc" />
      </View>
    );
  }

  return (
    <View style={styles.container}>
      <Text style={styles.title}>Список больниц</Text>
      
      {hospitals.length === 0 ? (
        <Text style={styles.empty}>В базе данных пока нет больниц</Text>
      ) : (
        <FlatList
          data={hospitals}
          keyExtractor={(item, index) => item.id ? item.id.toString() : index.toString()}
          renderItem={renderItem}
          showsVerticalScrollIndicator={false}
        />
      )}
    </View>
  );
};

const styles = StyleSheet.create({
  container: { flex: 1, padding: 20, backgroundColor: '#fff' },
  centered: { flex: 1, justifyContent: 'center', alignItems: 'center', backgroundColor: '#fff' },
  title: { fontSize: 24, fontWeight: 'bold', marginBottom: 20, color: '#0066cc', textAlign: 'center' },
  card: { padding: 15, backgroundColor: '#f0f4f8', borderRadius: 10, marginBottom: 15 },
  name: { fontSize: 18, fontWeight: 'bold', marginBottom: 5 },
  address: { fontSize: 14, color: '#666' },
  empty: { textAlign: 'center', fontSize: 16, color: '#999', marginTop: 50 }
});

// ВОТ ЭТА СТРОЧКА СПАСАЕТ ОТ ТВОЕЙ ОШИБКИ:
export default HospitalScreen;