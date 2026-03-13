import React, { useState } from 'react';
import { View, Text, StyleSheet, TextInput, TouchableOpacity, ScrollView, Alert } from 'react-native';

// ВНИМАНИЕ: Снова впиши свой IP! (Например 192.168.1.5)
const BASE_URL = 'http://192.168.0.103:44329/api';

const RegisterScreen = ({ navigation }) => {
  const [role, setRole] = useState('patient'); // 'patient' или 'doctor'
  
  // Добавили состояния для всех полей
  const [name, setName] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [specialization, setSpecialization] = useState('');

  const handleRegister = async () => {
    // Базовая проверка, что поля не пустые
    if (!name || !email || !password) {
      Alert.alert('Ошибка', 'Заполните все основные поля!');
      return;
    }
    if (role === 'doctor' && !specialization) {
      Alert.alert('Ошибка', 'Врачу необходимо указать специализацию!');
      return;
    }

    try {
      // Формируем данные для отправки на сервер
      const payload = {
        name: name,
        email: email,
        password: password,
        role: role,
        specialization: role === 'doctor' ? specialization : null
      };

      const response = await fetch(`${BASE_URL}/auth/register`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload)
      });

      if (response.ok) {
        Alert.alert('Ура!', 'Аккаунт успешно создан!');
        // После регистрации логично отправить на экран входа (или сразу домой)
        navigation.replace('Home'); 
      } else {
        // Если сервер ответил ошибкой (например, пароль слишком простой)
        Alert.alert(
          'Ошибка сервера', 
          'Проверьте данные. Пароль должен быть от 7 символов, содержать 1 БОЛЬШУЮ букву, цифру и спецсимвол (!@#).'
        );
      }
    } catch (error) {
      console.error(error);
      Alert.alert('Ошибка сети', 'Не удалось достучаться до сервера. Проверь IP!');
    }
  };

  return (
    <ScrollView contentContainerStyle={styles.container}>
      <Text style={styles.title}>Регистрация</Text>

      <View style={styles.roleContainer}>
        <TouchableOpacity 
          style={[styles.roleButton, role === 'patient' && styles.activeRole]} 
          onPress={() => setRole('patient')}
        >
          <Text style={[styles.roleText, role === 'patient' && styles.activeRoleText]}>Я Пациент</Text>
        </TouchableOpacity>
        <TouchableOpacity 
          style={[styles.roleButton, role === 'doctor' && styles.activeRole]} 
          onPress={() => setRole('doctor')}
        >
          <Text style={[styles.roleText, role === 'doctor' && styles.activeRoleText]}>Я Врач</Text>
        </TouchableOpacity>
      </View>

      <TextInput 
        style={styles.input} 
        placeholder="Полное имя" 
        value={name}
        onChangeText={setName}
      />
      
      <TextInput 
        style={styles.input} 
        placeholder="Email" 
        autoCapitalize="none" 
        keyboardType="email-address"
        value={email}
        onChangeText={setEmail}
      />
      
      <TextInput 
        style={styles.input} 
        placeholder="Пароль (мин 7 симв., 1 заглавная, 1 спецсимвол)" 
        secureTextEntry 
        value={password}
        onChangeText={setPassword}
      />
      
      {role === 'doctor' && (
        <TextInput 
          style={styles.input} 
          placeholder="Специализация (например, Кардиолог)" 
          value={specialization}
          onChangeText={setSpecialization}
        />
      )}

      <TouchableOpacity style={styles.button} onPress={handleRegister}>
        <Text style={styles.buttonText}>Создать аккаунт</Text>
      </TouchableOpacity>
    </ScrollView>
  );
};

const styles = StyleSheet.create({
  container: { padding: 30, backgroundColor: '#fff', flexGrow: 1, justifyContent: 'center' },
  title: { fontSize: 28, fontWeight: 'bold', marginBottom: 30, textAlign: 'center' },
  roleContainer: { flexDirection: 'row', marginBottom: 20, gap: 10 },
  roleButton: { flex: 1, padding: 12, borderRadius: 8, borderWidth: 1, borderColor: '#0066cc', alignItems: 'center' },
  activeRole: { backgroundColor: '#0066cc' },
  roleText: { color: '#0066cc', fontWeight: 'bold' },
  activeRoleText: { color: '#fff' },
  input: { backgroundColor: '#f0f4f8', padding: 15, borderRadius: 10, marginBottom: 15 },
  button: { backgroundColor: '#28a745', padding: 18, borderRadius: 10, alignItems: 'center', marginTop: 10 },
  buttonText: { color: '#fff', fontSize: 18, fontWeight: 'bold' },
});

export default RegisterScreen;