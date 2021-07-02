<template>
  <div id="SignUp">
    <div class="signUp">
      <InputModel v-for="user in userInfoList" v-bind:key="user.id" v-bind:object="user" v-on:inputResponse="inputValue">
      </InputModel>
    </div>
  </div>
</template>

<script>
import InputModel from './InputModel.vue'
export default {
  props: ['userInfo'],
  components: { InputModel },
  name: 'SignUp',
  data () {
    return {
      userInfoList: [{
        id: 1,
        label: '用户名',
        value: '',
        content: '',
        vonType: ''
      }, {
        id: 2,
        label: '密码',
        value: '',
        content: '',
        vonType: ''
      }, {
        id: 3,
        label: '确认密码',
        value: '',
        content: '',
        vonType: ''
      }, {
        id: 4,
        label: '手机号',
        value: '',
        content: '',
        vonType: ''
      }, {
        id: 5,
        label: '邮箱',
        value: '',
        content: '',
        vonType: ''
      }, {
        id: 6,
        label: '姓名',
        value: '',
        content: '',
        vonType: ''
      }, {
        id: 7,
        label: '性别',
        value: '',
        content: '',
        vonType: ''
      }, {
        id: 8,
        label: '年龄',
        value: '',
        content: '',
        vonType: ''
      }],
      inputWidth: '0px',
      divWidth: '0px'
    }
  },
  methods: {
    initDom () {
      this.divWidth = (this.$refs.divPSignUp.clientWidth - this.$refs.divU.clientWidth - 8) + 'px'
      this.inputWidth = (this.$refs.divPSignUp.clientWidth - this.$refs.divU.clientWidth - 8 - 60) + 'px'
    },
    inputValue (id, value) {
      if (id === 1) {
        this.userInfo.userName = value
      } else if (id === 2) {
        this.userInfo.password = value
      } else if (id === 3) {
        this.userInfo.enterPassword = value
      } else if (id === 4) {
        this.userInfo.phone = value
      } else if (id === 5) {
        this.userInfo.email = value
      } else if (id === 6) {
        this.userInfo.realName = value
      } else if (id === 7) {
        this.userInfo.sex = value
      } else {
        this.userInfo.age = value
      }
    }
  },
  updated () {
    this.initDom()
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.signUp{
  width: 100%;
  text-align: center;
  margin: auto;
}
</style>
