import React, { useState, useEffect } from 'react';
import { View, Text, StyleSheet, FlatList, ActivityIndicator } from 'react-native';
import apiClient from '../api/client';

const AppointmentsScreen = () => {
  const [appointments, setAppointments] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchAppointments = async () => {
      try {
        const response = await apiClient.get('/appointments/my'); // Путь к записям текущего юзера
        setAppointments(response.data);
      } catch (error) {
        console.log("Записи пока не подгрузились, используем пустой список");
      } finally {
        setLoading(false);
      }
    };
    fetchAppointments();
  }, []);

  if (loading) return <ActivityIndicator style={{flex:1}} />;

  return (
    <View style={styles.container}>
      <Text style={styles.header}>Мои записи</Text>
      <FlatList
        data={appointments}
        keyExtractor={(item) => item.id.toString()}
        renderItem={({ item }) => (
          <View style={styles.card}>
            <Text style={styles.date}>{item.date}</Text>
            <Text style={styles.doctor}>{item.doctorName}</Text>
            <Text style={styles.status}>{item.status}</Text>
          </View>
        )}
        ListEmptyComponent={<Text style={styles.empty}>У вас пока нет активных записей</Text>}
      />
    </View>
  );
};

const styles = StyleSheet.create({
  container: { flex: 1, padding: 20, backgroundColor: '#fff' },
  header: { fontSize: 22, fontWeight: 'bold', marginBottom: 20, marginTop: 40 },
  card: { padding: 15, borderRadius: 10, backgroundColor: '#f9f9f9', marginBottom: 10, borderLeftWidth: 4, borderLeftColor: '#28a745' },
  date: { fontWeight: 'bold', fontSize: 16 },
  doctor: { color: '#555', marginVertical: 4 },
  status: { color: '#28a745', fontSize: 12, fontWeight: '600' },
  empty: { textAlign: 'center', color: '#999', marginTop: 40 }
});

export default AppointmentsScreen;  