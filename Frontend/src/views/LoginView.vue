<template>
  <div style="display: flex; flex-direction: row; flex-wrap: nowrap; justify-content:space-around;">
    <div class="login-part">
      <h2>Вход</h2>
      <label class="form-label">Логин</label>
      <input v-model="loginCommand.login" type="text">
      <span class="error-label" v-if="(loginValidationErrors.loginError != null)">{{loginValidationErrors.loginError}}</span>
      <label class="form-label">Пароль</label>
      <input v-model="loginCommand.password" type="password">
      <span class="error-label" v-if="(loginValidationErrors.passwordError != null)">{{loginValidationErrors.passwordError}}</span>
      <div class="confirm-button" @click="loginUser">Войти</div>
    </div>
    <div class="register-part">
      <h2>Регистрация</h2>
      <label class="form-label">Логин</label>
      <input v-model="registerCommand.login" type="text">
      <span class="error-label" v-if="(registerValidationErrors.loginError != null)">{{registerValidationErrors.loginError}}</span>
      <label class="form-label">Имя</label>
      <input v-model="registerCommand.name" type="text">
      <span class="error-label" v-if="(registerValidationErrors.nameError != null)">{{registerValidationErrors.nameError}}</span>
      <label class="form-label">Фамилия</label>
      <input v-model="registerCommand.surname" type="text">
      <span class="error-label" v-if="(registerValidationErrors.surnameError != null)">{{registerValidationErrors.surnameError}}</span>
      <label class="form-label">Пароль</label>
      <input v-model="registerCommand.password" type="password">
      <span class="error-label" v-if="(registerValidationErrors.passwordError != null)">{{registerValidationErrors.passwordError}}</span>
      <label class="form-label">Подтверждение пароля</label>
      <input v-model="passwordConfirmation" type="password">
      <span class="error-label" v-if="(passwordConfirmationError != null)">{{passwordConfirmationError}}</span>
      <div class="confirm-button" @click="registerUser">Зарегестрироваться</div>
    </div>
  </div>
</template>

<script>
import { login, register } from "../boot/axios"

export default {
  name: "Login",
  props: {
    logined : Boolean
  },
  data(){
    return{
      loginCommand:{
        login: "",
        password: ""
      },
      loginValidationErrors:{
        loginError: null,
        passwordError: null
      },
      registerCommand:{
        login: "",
        password: "",
        name: "",
        surname: ""
      },
      registerValidationErrors:{
        loginError: null,
        passwordError: null,
        nameError: null,
        surnameError: null
      },
      passwordConfirmation : "",
      passwordConfirmationError: null
    }
  },
  beforeMount(){
    if (this.logined) {
      this.$router.push('/')
    }
  },
  methods: {
    async loginUser(){
      if(!this.checkLoginData()) return;
      try {
        const response = await login(this.loginCommand);
        console.log(response.data);
        if (response.data.isSuccess) {
          this.$emit('changeLoginedState', true)
          this.$router.push('/')
        }
        else if (response.data.exceptionCode) {
          let message = ""
          switch (response.data.exceptionCode) {
            case 1:
              message += "Ошибка введённых данных " + response.data.exception
              break;
            case 2:
              message += "Пользователь не имеет доступа к данному действию"
              break;
            case 3:
              message += "Пользователю необходимо авторизоваться"
              break;
            case 4:
              message += "Ошибка записи данных в БД " + response.data.exception
              break;
          }
          this.$notify(message)
        }
      } catch (error) {
        console.log(error.message);
      }
    },
    async registerUser(){
      if(!this.checkRegisterData()) return;
      try {
        const response = await register(this.registerCommand);
        console.log(response.data);
        if (response.data.isSuccess) {
          this.$emit('changeLoginedState', true)
          this.$router.push('/')
        }
        else if (response.data.exceptionCode) {
          let message = ""
          switch (response.data.exceptionCode) {
            case 1:
              message += "Ошибка введённых данных " + response.data.exception
              break;
            case 2:
              message += "Пользователь не имеет доступа к данному действию"
              break;
            case 3:
              message += "Пользователю необходимо авторизоваться"
              break;
            case 4:
              message += "Ошибка записи данных в БД " + response.data.exception
              break;
          }
          this.$notify(message)
        }
      } catch (error) {
        console.log(error.message);
      }
    },
    checkLoginData(){
      let result = true;
      if(this.loginCommand.login.length < 3 || this.loginCommand.login.length > 30)
      {
        this.loginValidationErrors.loginError = "Логин не может быть короче 3-х символов или длинее 30"
        result = false
      }
      else{
        this.loginValidationErrors.loginError = null
      }
      
      if(this.loginCommand.password.length < 6 || this.loginCommand.password.length > 30)
      {
        this.loginValidationErrors.passwordError = "Пароль не может быть короче 6 символов или длинее 30"
        result = false
      }
      else{
        this.loginValidationErrors.passwordError = null
      }

      return result
    },
    checkRegisterData(){
      let result = true;
      
      console.log(this.registerCommand.login.length)
      if(this.registerCommand.login.length < 3 || this.registerCommand.login.length > 30)
      {
        this.registerValidationErrors.loginError = "Логин не может быть короче 3-х символов или длинее 30"
        result = false
      }
      else{
        this.registerValidationErrors.loginError = null
      }

      if(this.registerCommand.password.length < 6 || this.registerCommand.password.length > 30)
      {
        this.registerValidationErrors.passwordError = "Пароль не может быть короче 6 символов или длинее 30"
        result = false
      }
      else{
        this.registerValidationErrors.passwordError = null
      }

      if(this.registerCommand.name.length < 2 || this.registerCommand.name.length > 30)
      {
        this.registerValidationErrors.nameError = "Имя пользователя не может быть короче 2-х символов или длинее 30"
        result = false
      }
      else{
        this.registerValidationErrors.nameError = null
      }

      if(this.registerCommand.surname.length < 2 || this.registerCommand.surname.length > 30)
      {
        this.registerValidationErrors.surnameError = "Фамилия не может быть короче 2-х символов или длинее 30"
        result = false
      }
      else{
        this.registerValidationErrors.surnameError = null
      }

      if(this.registerCommand.password != this.passwordConfirmation)
      {
        this.passwordConfirmationError = "Пароли не совпадают"
        result = false
      }
      else{
        this.passwordConfirmationError = null
      }
      return result
    }
  }
}
</script>

<style scoped>
.login-part{
  display: flex;
  flex-direction: column;
  width: 400px;
  text-align: center;
}

.register-part{
  display: flex;
  flex-direction: column;
  width: 400px;
  text-align: center;
}

.form-label{
  margin-bottom: 5px;
  margin-top: 10px;
}
.confirm-button{
  margin-top: 20px;
  background-color: blue;
  color: white;
  border: 0px;
  border-radius: 10px;
  padding: 15px;
  cursor: pointer;
}

.confirm-button:hover{
  background-color: rgb(51, 103, 199);
  padding: 20px, 10px;
}

.error-label{
  color: red;
  font-size: small;
}
</style>