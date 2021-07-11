<template>
  <div id="MainPage" ref="page">
    <MainModule v-if="showType==='Main'" v-bind:loginUser="loginUserId" v-on:buttonClick="buttonClick"></MainModule>
    <LoginPage v-else-if="showType==='Login'"></LoginPage>
    <Recode v-else-if="showType==='Record'"></Recode>
  </div>
</template>

<script>
import LoginPage from './LoginModule/LoginPage.vue'
import Record from './RecordModule/Record.vue'
import MainModule from './MainModule/MainModule.vue'
export default {
  components: { LoginPage, Record, MainModule },
  name: 'MainPage',
  data () {
    return {
      top: '0px',
      showType: 'Main',
      loginUserId: {
        id: ''
      }
    }
  },
  methods: {
    initDom () {
    },
    buttonClick (type) {
      if (type === '0') {
        this.showType = 'Login'
      } else if (type === '1') {
        this.showType = 'Record'
      } else if (type === '2') {
        this.showType = 'Family'
      } else if (type === '3') {
        this.showType = 'Bill'
      }
    }
  },
  mounted () {
    this.initDom()
  }
}
</script>

<style scoped>
#MainPage {
  width: 100%;
  height: 100%;
}
</style>
