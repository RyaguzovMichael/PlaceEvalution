<template>
  <router-view :logined="logined" @changeLoginedState="changeLoginedState"/>
</template>

<script>
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
  methods: {
    async checkLogin() {
      try {
        const response = await checkSessionAuthorization();
        console.log(response.data);
        if (response.data.isSuccess) {
          this.logined = response.data.value
        }
        else if (response.data.exceptionCode) {
          switch (response.data.exceptionCode) {

          }
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
