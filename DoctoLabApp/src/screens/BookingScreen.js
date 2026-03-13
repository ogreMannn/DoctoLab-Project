import React, { useState } from 'react';
import { View, Text, StyleSheet, TouchableOpacity, ScrollView, Alert } from 'react-native';

const DATES = ['12 Мая', '13 Мая', '14 Мая', '15 Мая', '16 Мая'];
const SLOTS = ['09:00', '10:00', '11:00', '14:00', '15:00', '16:00', '17:00'];

const BookingScreen = ({ route, navigation }) => {
  const { doctor } = route.params;
  const [selectedDate, setSelectedDate] = useState(DATES[0]);
  const [selectedTime, setSelectedTime] = useState(null);

  const handleConfirm = () => {
    if (!selectedTime) {
      Alert.alert('Внимание', 'Пожалуйста, выберите время');
      return;
    }
    Alert.alert(
      'Запись создана!',
      `Вы записаны к д-ру ${doctor.name} на ${selectedDate} в ${selectedTime}.`,
      [{ text: 'OK', onPress: () => navigation.navigate('Home') }]
    );
  };

  return (
    <View style={styles.container}>
      <ScrollView>
        <View style={styles.header}>
          <Text style={styles.doctorName}>Д-р {doctor.name}</Text>
        </View>

        <Text style={styles.sectionTitle}>Выберите дату</Text>
        <ScrollView horizontal showsHorizontalScrollIndicator={false} style={styles.dateList}>
          {DATES.map(date => (
            <TouchableOpacity 
              key={date} 
              style={[styles.dateCard, selectedDate === date && styles.selectedCard]}
              onPress={() => setSelectedDate(date)}
            >
              <Text style={[styles.dateText, selectedDate === date && styles.selectedText]}>{date}</Text>
            </TouchableOpacity>
          ))}
        </ScrollView>

        <Text style={styles.sectionTitle}>Доступное время</Text>
        <View style={styles.timeGrid}>
          {SLOTS.map(time => (
            <TouchableOpacity 
              key={time} 
              style={[styles.timeSlot, selectedTime === time && styles.selectedCard]}
              onPress={() => setSelectedTime(time)}
            >
              <Text style={[styles.timeText, selectedTime === time && styles.selectedText]}>{time}</Text>
            </TouchableOpacity>
          ))}
        </View>
      </ScrollView>

      <View style={styles.footer}>
        <TouchableOpacity style={styles.confirmButton} onPress={handleConfirm}>
          <Text style={styles.confirmButtonText}>Подтвердить запись</Text>
        </TouchableOpacity>
      </View>
    </View>
  );
};

const styles = StyleSheet.create({
  container: { flex: 1, backgroundColor: '#f8f9fa' },
  header: { padding: 20, backgroundColor: '#fff', borderBottomWidth: 1, borderBottomColor: '#eee' },
  doctorName: { fontSize: 22, fontWeight: 'bold', color: '#333' },
  sectionTitle: { fontSize: 18, fontWeight: 'bold', margin: 20, marginBottom: 10 },
  dateList: { paddingLeft: 20 },
  dateCard: { backgroundColor: '#fff', padding: 15, borderRadius: 12, marginRight: 10, borderWidth: 1, borderColor: '#eee', width: 90, alignItems: 'center' },
  timeGrid: { flexDirection: 'row', flexWrap: 'wrap', padding: 15, justifyContent: 'space-between' },
  timeSlot: { backgroundColor: '#fff', width: '30%', padding: 15, borderRadius: 12, marginBottom: 10, alignItems: 'center', borderWidth: 1, borderColor: '#eee' },
  selectedCard: { backgroundColor: '#0066cc', borderColor: '#0066cc' },
  selectedText: { color: '#fff', fontWeight: 'bold' },
  dateText: { color: '#333' },
  timeText: { color: '#333' },
  footer: { padding: 20, backgroundColor: '#fff', paddingBottom: 30 },
  confirmButton: { backgroundColor: '#28a745', padding: 18, borderRadius: 12, alignItems: 'center' },
  confirmButtonText: { color: '#fff', fontSize: 18, fontWeight: 'bold' },
});

export default BookingScreen; 