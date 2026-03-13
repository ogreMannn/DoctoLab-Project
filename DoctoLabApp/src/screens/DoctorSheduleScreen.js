import React, { useState, useEffect } from 'react';
import { View, Text, StyleSheet, FlatList, ActivityIndicator, Alert, RefreshControl } from 'react-native';
import apiClient from '../api/client'; // Импортируем наш настроенный клиент

const DoctorScheduleScreen = () => {
  const [appointments, setAppointments] = useState([]);
  const [loading, setLoading] = useState(true);
  const [refreshing, setRefreshing] = useState(false);

  // Функция загрузки данных
  const fetchSchedule = async () => {
    try {
      // Запрос к бэкенду. Токен подставится автоматически через наш apiClient
      const response = await apiClient.get('/appointments/doctor'); 
      setAppointments(response.data);
    } catch (error) {
      console.error(error);
      // Пока данных в БД может не быть, не будем пугать ошибкой, просто выведем в консоль
    } finally {
      setLoading(false);
      setRefreshing(false);
    }
  };

  useEffect(() => {
    fetchSchedule();
  }, []);

  // Функция для обновления списка "потяни, чтобы обновить"
  const onRefresh = () => {
    setRefreshing(true);
    fetchSchedule();
  };

  if (loading) {
    return (
      <View style={styles.center}>
        <ActivityIndicator size="large" color="#0066cc" />
        <Text>Загрузка расписания...</Text>
      </View>
    );
  }

  return (
    <View style={styles.container}>
      <Text style={styles.header}>Мое расписание</Text>
      
      <FlatList
        data={appointments}
        keyExtractor={(item) => item.id.toString()}
        refreshControl={
          <RefreshControl refreshing={refreshing} onRefresh={onRefresh} />
        }
        renderItem={({ item }) => (
          <View style={styles.card}>
            {/* Предполагаем, что в модели Appointment есть поля Time, PatientName и Type */}
            <Text style={styles.time}>{item.time || '00:00'}</Text>
            <View>
              <Text style={styles.patient}>{item.patientName || 'Анонимный пациент'}</Text>
              <Text style={styles.type}>{item.type || 'Прием'}</Text>
            </View>
          </View>
        )}
        ListEmptyComponent={
          <View style={styles.emptyContainer}>
            <Text style={styles.emptyText}>На сегодня записей пока нет</Text>
          </View>
        }
      />
    </View>
  );
};

const styles = StyleSheet.create({
  container: { flex: 1, padding: 20, backgroundColor: '#f8f9fa' },
  center: { flex: 1, justifyContent: 'center', alignItems: 'center' },
  header: { fontSize: 22, fontWeight: 'bold', marginBottom: 20, marginTop: 40, color: '#1a1a1a' },
  card: { 
    flexDirection: 'row', 
    backgroundColor: '#fff', 
    padding: 15, 
    borderRadius: 12, 
    marginBottom: 10,
    alignItems: 'center',
    elevation: 2,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 1 },
    shadowOpacity: 0.1,
    shadowRadius: 2,
  },
  time: { fontSize: 18, fontWeight: 'bold', color: '#0066cc', marginRight: 20 },
  patient: { fontSize: 16, fontWeight: '600', color: '#333' },
  type: { color: '#666', fontSize: 14 },
  emptyContainer: { marginTop: 50, alignItems: 'center' },
  emptyText: { color: '#999', fontSize: 16 }
});

export default DoctorScheduleScreen;