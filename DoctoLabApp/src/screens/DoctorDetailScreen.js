import React from 'react';
import { View, Text, StyleSheet, TouchableOpacity, ScrollView, Platform, StatusBar } from 'react-native';
import { Ionicons } from '@expo/vector-icons';

const DoctorDetailsScreen = ({ route, navigation }) => {
  // БЕЗОПАСНОЕ ИЗВЛЕЧЕНИЕ: проверяем, есть ли вообще params и doctor внутри
  const doctor = route?.params?.doctor;

  // ЗАЩИТА ОТ КРАША: Если данные врача не пришли, показываем красивое сообщение об ошибке
  if (!doctor) {
    return (
      <View style={styles.errorContainer}>
        <Ionicons name="warning-outline" size={80} color="#cc0000" />
        <Text style={styles.errorText}>Упс! Данные врача не найдены.</Text>
        <Text style={styles.errorSubText}>Похоже, вы перешли на этот экран без передачи объекта врача.</Text>
        <TouchableOpacity 
          style={styles.errorButton} 
          onPress={() => navigation.goBack()}
        >
          <Text style={styles.errorButtonText}>Вернуться назад</Text>
        </TouchableOpacity>
      </View>
    );
  }

  // Если всё хорошо, рендерим твой интерфейс
  return (
    <View style={styles.safeArea}>
      <ScrollView contentContainerStyle={styles.container} showsVerticalScrollIndicator={false}>
        
        {/* Кнопка "Назад" */}
        <TouchableOpacity style={styles.backButton} onPress={() => navigation.goBack()}>
          <Ionicons name="arrow-back" size={24} color="#333" />
        </TouchableOpacity>

        <View style={styles.header}>
          <View style={styles.avatarLarge}>
            <Ionicons name="person" size={60} color="#0066cc" />
          </View>
          {/* Используем fullName (как обычно в C# моделях) или name */}
          <Text style={styles.name}>Д-р {doctor.fullName || doctor.name}</Text>
          <Text style={styles.specialty}>{doctor.specialization || doctor.specialty?.name || 'Специалист'}</Text>
          <Text style={styles.hospital}>{doctor.hospitalName || doctor.hospital?.name || 'Медцентр DoctoLab'}</Text>
        </View>

        <View style={styles.infoSection}>
          <Text style={styles.sectionTitle}>О враче</Text>
          <Text style={styles.description}>
            {doctor.description || 
            "Опытный специалист с многолетним стажем работы. Проводит тщательную диагностику и назначает эффективное лечение в соответствии с международными стандартами."}
          </Text>
          
          <View style={styles.statsContainer}>
             <View style={styles.statBox}>
               <Text style={styles.statValue}>{doctor.experience || '5+'}</Text>
               <Text style={styles.statLabel}>лет стажа</Text>
             </View>
             <View style={styles.statBox}>
               <Text style={styles.statValue}>4.9</Text>
               <Text style={styles.statLabel}>Рейтинг</Text>
             </View>
          </View>
        </View>
      </ScrollView>

      <View style={styles.footer}>
        <TouchableOpacity 
          style={styles.bookButton}
          activeOpacity={0.8}
          onPress={() => navigation.navigate('Booking', { doctorId: doctor.id, doctorName: doctor.fullName || doctor.name })}
        >
          <Text style={styles.bookButtonText}>Записаться на прием</Text>
        </TouchableOpacity>
      </View>
    </View>
  );
};

const styles = StyleSheet.create({
  safeArea: { flex: 1, backgroundColor: '#f8f9fa', paddingTop: Platform.OS === 'android' ? (StatusBar.currentHeight || 40) : 0 },
  container: { padding: 20 },
  backButton: { marginBottom: 10, width: 40, height: 40, justifyContent: 'center' },
  header: { alignItems: 'center', marginBottom: 30 },
  avatarLarge: {
    width: 120, height: 120, borderRadius: 60, backgroundColor: '#e6f0fa',
    justifyContent: 'center', alignItems: 'center', marginBottom: 16,
    elevation: 4, shadowColor: '#000', shadowOpacity: 0.1, shadowRadius: 8,
  },
  name: { fontSize: 24, fontWeight: 'bold', color: '#333', marginBottom: 8, textAlign: 'center' },
  specialty: { fontSize: 18, color: '#0066cc', marginBottom: 4, textAlign: 'center' },
  hospital: { fontSize: 14, color: '#888', textAlign: 'center' },
  infoSection: { backgroundColor: '#fff', padding: 20, borderRadius: 16, elevation: 2 },
  sectionTitle: { fontSize: 18, fontWeight: 'bold', color: '#333', marginBottom: 12 },
  description: { fontSize: 15, color: '#666', lineHeight: 22 },
  statsContainer: { flexDirection: 'row', justifyContent: 'space-around', marginTop: 20, borderTopWidth: 1, borderTopColor: '#eee', paddingTop: 20 },
  statBox: { alignItems: 'center' },
  statValue: { fontSize: 18, fontWeight: 'bold', color: '#0066cc' },
  statLabel: { fontSize: 12, color: '#999' },
  footer: { padding: 20, backgroundColor: '#fff', borderTopWidth: 1, borderTopColor: '#eee', paddingBottom: Platform.OS === 'ios' ? 40 : 20 },
  bookButton: { backgroundColor: '#0066cc', paddingVertical: 16, borderRadius: 12, alignItems: 'center' },
  bookButtonText: { color: '#fff', fontSize: 18, fontWeight: 'bold' },
  
  // Новые стили для экрана ошибки
  errorContainer: { flex: 1, justifyContent: 'center', alignItems: 'center', backgroundColor: '#f8f9fa', padding: 24 },
  errorText: { fontSize: 20, fontWeight: 'bold', marginTop: 20, color: '#333', textAlign: 'center' },
  errorSubText: { fontSize: 14, color: '#666', marginTop: 10, textAlign: 'center', marginBottom: 30 },
  errorButton: { paddingVertical: 14, paddingHorizontal: 24, backgroundColor: '#0066cc', borderRadius: 12 },
  errorButtonText: { color: '#fff', fontSize: 16, fontWeight: 'bold' }
});

export default DoctorDetailsScreen;