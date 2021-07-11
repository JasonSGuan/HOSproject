<template>
  <div id="Login">
    <div class="login">
      <InputModel v-for="user in userInfoList" v-bind:key="user.id" v-bind:object="user" v-on:divResponse="changePassword" v-on:inputResponse="inputValue">
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
        content: '',
        vonType: ''
      }, {
        id: 2,
        label: '密码',
        value: '',
        content: '忘记密码',
        vonType: 'divClick'
      }]
    }
  },
  methods: {
    initDom () {
      this.userInfoList[0].value = this.userInfo.userName
      this.userInfoList[1].value = this.userInfo.password
    },
    changePassword () {
      this.$emit('forgetPassword')
    },
    inputValue (id, value) {
      if (id === 1) {
        this.userInfo.userName = value
        let reg = /^[a-zA-Z]{5,20}$/
        let pattern = new RegExp(reg)
        if (!pattern.test(value)) {
          this.userInfoList[0].content = '用户名只能由字母组成，长度不超过5-20位'
        } else {
          this.userInfoList[0].content = ''
          /* this.request({
            method: 'post',
            url: '/Login/Repeated',
            data: { 'userName': this.userInfo.userName },
            headers: {'Content-Type': 'application/json'}
          }).then((res) => {
            if (res.data.Status === '2000') {
              this.userInfoList[0].content = '用户名不存在'
            } else {
              this.userInfoList[0].content = ''
            }
          }).catch(function (err) {
            alert(err)
          }) */
        }
      } else {
        this.userInfo.password = value
      }
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
