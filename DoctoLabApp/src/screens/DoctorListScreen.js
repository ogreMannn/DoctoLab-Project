import React, { useState, useEffect } from 'react';
import { View, Text, StyleSheet, FlatList, TouchableOpacity, ActivityIndicator, Alert } from 'react-native';
import { Ionicons } from '@expo/vector-icons';
import apiClient from '../api/client'; // Используем наш настроенный клиент

const DoctorListScreen = ({ navigation }) => {
  const [doctors, setDoctors] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    fetchDoctors();
  }, []);

  const fetchDoctors = async () => {
    try {
      const response = await apiClient.get('/doctors'); // Твой эндпоинт на бэке
      setDoctors(response.data);
    } catch (error) {
      console.error(error);
      Alert.alert('Ошибка', 'Не удалось загрузить список врачей');
    } finally {
      setLoading(false);
    }
  };

 const renderDoctor = ({ item }) => (
    <TouchableOpacity 
      style={styles.card}
      onPress={() => navigation.navigate('DoctorDetail', { doctor: item })}
    >
      <View style={styles.avatar}>
        <Ionicons name="person" size={30} color="#0066cc" />
      </View>
      <View style={styles.info}>
        <Text style={styles.name}>{item.fullName || item.name}</Text>
        <Text style={styles.specialization}>{item.specialization}</Text>
        <Text style={styles.experience}>Стаж: {item.experience} лет</Text>
      </View>
      <Ionicons name="chevron-forward" size={20} color="#ccc" />
    </TouchableOpacity>
  );

  if (loading) {
    return <ActivityIndicator size="large" color="#0066cc" style={{ flex: 1 }} />;
  }

  return (
    <View style={styles.container}>
      <Text style={styles.header}>Наши специалисты</Text>
      <FlatList
        data={doctors}
        keyExtractor={(item) => item.id.toString()}
        renderItem={renderDoctor}
        ListEmptyComponent={<Text style={styles.empty}>Врачи не найдены</Text>}
      />
    </View>
  );
};

const styles = StyleSheet.create({
  container: { flex: 1, backgroundColor: '#f8f9fa', padding: 20 },
  header: { fontSize: 24, fontWeight: 'bold', marginBottom: 20, marginTop: 30 },
  card: { 
    flexDirection: 'row', backgroundColor: '#fff', padding: 15, 
    borderRadius: 12, marginBottom: 12, alignItems: 'center', elevation: 2 
  },
  avatar: { width: 50, height: 50, borderRadius: 25, backgroundColor: '#e6f0fa', justifyContent: 'center', alignItems: 'center', marginRight: 15 },
  info: { flex: 1 },
  name: { fontSize: 18, fontWeight: 'bold' },
  specialization: { color: '#0066cc', marginBottom: 2 },
  experience: { color: '#666', fontSize: 12 },
  empty: { textAlign: 'center', marginTop: 50, color: '#999' }
});

export default DoctorListScreen;