import React from 'react';
import { View, Text, StyleSheet, TouchableOpacity, Platform, StatusBar } from 'react-native';
import { Ionicons } from '@expo/vector-icons';

const HomeScreen = ({ navigation }) => {
  return (
    <View style={styles.safeArea}>
      <View style={styles.container}>
        
        {/* Шапка */}
        <View style={styles.header}>
          <View>
            <Text style={styles.greeting}>Добро пожаловать в</Text>
            <Text style={styles.brandName}>DoctoLab</Text>
          </View>
          
          {/* ИСПРАВЛЕНО: Теперь иконка профиля кликабельна и ведет на Логин */}
          <TouchableOpacity 
            style={styles.avatarPlaceholder}
            onPress={() => navigation.navigate('Login')}
          >
            <Ionicons name="person" size={24} color="#0066cc" />
          </TouchableOpacity>
        </View>

        <Text style={styles.sectionTitle}>Что вас беспокоит?</Text>

        <View style={styles.menuContainer}>
          
          {/* КАРТОЧКА: НАЙТИ ВРАЧА */}
          <TouchableOpacity 
            style={styles.card} 
            activeOpacity={0.8}
            onPress={() => navigation.navigate('Doctors')} // Убедись, что этот экран есть в навигаторе
          >
            <View style={[styles.iconContainer, { backgroundColor: '#e6f0fa' }]}>
              <Ionicons name="medkit" size={32} color="#0066cc" />
            </View>
            <View style={styles.cardText}>
              <Text style={styles.cardTitle}>Найти врача</Text>
              <Text style={styles.cardSubtitle}>Специалисты и запись</Text>
            </View>
            <Ionicons name="chevron-forward" size={24} color="#ccc" />
          </TouchableOpacity>

          {/* КАРТОЧКА: МОИ ЗАПИСИ */}
          <TouchableOpacity 
            style={styles.card} 
            activeOpacity={0.8}
            onPress={() => navigation.navigate('Appointments')} 
          >
            <View style={[styles.iconContainer, { backgroundColor: '#e8f5e9' }]}>
              <Ionicons name="calendar" size={32} color="#28a745" />
            </View>
            <View style={styles.cardText}>
              <Text style={styles.cardTitle}>Мои записи</Text>
              <Text style={styles.cardSubtitle}>Предстоящие приемы</Text>
            </View>
            <Ionicons name="chevron-forward" size={24} color="#ccc" />
          </TouchableOpacity>

          {/* КАРТОЧКА: ГОСПИТАЛИ */}
          <TouchableOpacity 
            style={styles.card} 
            activeOpacity={0.8}
            onPress={() => navigation.navigate('Hospitals')} 
          >
            <View style={[styles.iconContainer, { backgroundColor: '#f0f9eb' }]}>
              <Ionicons name="business" size={32} color="#67c23a" />
            </View>
            <View style={styles.cardText}>
              <Text style={styles.cardTitle}>Госпитали</Text>
              <Text style={styles.cardSubtitle}>Выбор по медцентрам</Text>
            </View>
            <Ionicons name="chevron-forward" size={24} color="#ccc" />
          </TouchableOpacity>

        </View>
      </View>
    </View>
  );
};

// Стили оставляем без изменений, они отличные
const styles = StyleSheet.create({
  safeArea: {
    flex: 1,
    backgroundColor: '#f8f9fa',
    paddingTop: Platform.OS === 'android' ? (StatusBar.currentHeight || 40) : 40,
  },
  container: { flex: 1, paddingHorizontal: 20 },
  header: { flexDirection: 'row', justifyContent: 'space-between', alignItems: 'center', marginTop: 20, marginBottom: 40 },
  greeting: { fontSize: 16, color: '#666', marginBottom: 4 },
  brandName: { fontSize: 28, fontWeight: 'bold', color: '#1a1a1a' },
  avatarPlaceholder: {
    width: 50, height: 50, borderRadius: 25, backgroundColor: '#fff',
    justifyContent: 'center', alignItems: 'center', elevation: 3,
    shadowColor: '#000', shadowOffset: { width: 0, height: 2 }, shadowOpacity: 0.1, shadowRadius: 4,
  },
  sectionTitle: { fontSize: 20, fontWeight: '700', color: '#333', marginBottom: 20 },
  menuContainer: { gap: 16 },
  card: {
    flexDirection: 'row', alignItems: 'center', backgroundColor: '#fff',
    padding: 20, borderRadius: 16, elevation: 2,
    shadowColor: '#000', shadowOffset: { width: 0, height: 1 }, shadowOpacity: 0.05, shadowRadius: 2,
  },
  iconContainer: { width: 60, height: 60, borderRadius: 12, justifyContent: 'center', alignItems: 'center', marginRight: 16 },
  cardText: { flex: 1 },
  cardTitle: { fontSize: 18, fontWeight: 'bold', color: '#333', marginBottom: 4 },
  cardSubtitle: { fontSize: 14, color: '#888' },
});

export default HomeScreen;