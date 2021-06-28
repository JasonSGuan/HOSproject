<template>
  <div id="Login" ref="page">
    <div class="login">
      <div class="userName" ref="divPLogin">
        <div class="lable" ref="divU">用户名：</div>
        <div class="divInput" :style="{ width: divWidth}">
          <input type="text" v-model="userName" class="input" :style="{ width: inputWidth}" />
        </div>
      </div>
      <div class="password">
        <div class="lable">密码：</div>
        <div class="divInput" :style="{ width: divWidth}">
          <input type="text" v-model="password" class="input" :style="{ width: inputWidth}" />
          <a @click="forgetPWDClick" class="a">忘记密码</a>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'Login',
  data () {
    return {
      userName: '',
      password: '',
      inputWidth: '0px',
      divWidth: '0px'
    }
  },
  methods: {
    initDom () {
      this.divWidth = (this.$refs.divPLogin.clientWidth - this.$refs.divU.clientWidth - 8) + 'px'
      this.inputWidth = (this.$refs.divPLogin.clientWidth - this.$refs.divU.clientWidth - 8 - 60) + 'px'
    },
    forgetPWDClick () {
      console.info(this.userName)
      console.info(this.password)
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
.userName{
  width: 90%;
  top: -40%;
  margin: auto;
}
.password{
  width: 90%;
  margin: auto;
  margin-top: 15px;
}
.lable{
  display: inline-block;
  width: 60px;
  text-align: right;
  font-size: 12px
}
.divInput{
  display: inline-block;
  text-align: left;
}
.input{
  border: 0;
  text-align: right;
  height: 30px;
  opacity: 0.75;
  background-color: rgb(225,255,230);
  padding: 0px;
}
.a{
  font-size: 12px;
  width: 20px;
}
</style>
