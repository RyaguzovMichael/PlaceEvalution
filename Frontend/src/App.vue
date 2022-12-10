<template>
  <router-view :logined="logined" @changeLoginedState="changeLoginedState"/>
  <notifications />
</template>

<script>
import { response } from "express";
import { checkSessionAuthorization } from "./boot/axios"

export default {
  name: 'Layout',
  data() {
    return {
      logined: false
    }
  },
  beforeMount() {
    this.checkLogin();
    
  },
  mounted(){
    
  },
  methods: {
    async checkLogin() {
      try {
        const response = await checkSessionAuthorization();
        console.log(response.data);
        if (response.data.isSuccess) {
          this.logined = response.data.value
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
    changeLoginedState(state) {
      this.logined = state;
    }
  }
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
}
</style>
