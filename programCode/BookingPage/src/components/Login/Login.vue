<template>
  <div id="Login">
    <div class="login">
      <InputModel v-for="user in userInfoList" v-bind:key="user.id" v-bind:object="user">
      </InputModel>
    </div>
  </div>
</template>

<script>
import InputModel from './InputModel.vue'
export default {
  props: ['userInfo'],
  components: { InputModel },
  name: 'Login',
  data () {
    return {
      userInfoList: [{
        id: 1,
        label: '用户名',
        value: '',
        content: ''
      }, {
        id: 2,
        label: '密码',
        value: '',
        content: '忘记密码'
      }],
      inputWidth: '0px',
      divWidth: '0px'
    }
  },
  methods: {
    initDom () {
      this.userInfoList[0].value = this.userInfo.userName
      this.userInfoList[1].value = this.userInfo.password
    }
  },
  mounted () {
    this.initDom()
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.login{
  width: 100%;
  text-align: center;
  margin: auto;
}
</style>
