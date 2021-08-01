<template>
  <div id="LoginPage" ref="page">
<!--     <div class="back" @click="backClick" :style="{ 'top': top}">
      <img src="~@/assets/返回.png" class="img" />
    </div> -->
    <Login class="login" ref="login" v-bind:userInfo="loginUserInfo" :style="{ 'margin-top': topIn}" v-if="pageType==='login'" v-on:forgetPassword="changePassword">
    </Login>
    <SignUp class="signUp" ref="signUp" v-bind:userInfo="signUpUserInfo" :style="{ 'margin-top': topUp}" v-else-if="pageType==='signUp'">
    </SignUp>
    <ForgetPWD class="forgetPwd" ref="forgetPwd" v-bind:userInfo="changePWDuserInfo" :style="{ 'margin-top': topF}" v-else-if="pageType==='forgetPWD'">
    </ForgetPWD>
    <div>
      <div class="inline margin">
        <button class="button" @click="signInClick">{{reback ? '提   交' : '登   录'}}</button>
      </div>
      <div class="inline margin">
        <button class="button" @click="signUpClick" >{{reback ? '返   回' : '注   册'}}</button>
      </div>
    </div>
  </div>
</template>

<script>
import Login from './Login.vue'
import SignUp from './SignUp.vue'
import ForgetPWD from './ForgetPWD.vue'
export default {
  components: { Login, SignUp, ForgetPWD },
  name: 'LoginPage',
  data () {
    return {
      loginUserInfo: {
        userName: '',
        password: ''
      },
      signUpUserInfo: {
        userName: '',
        password: '',
        enterPassword: '',
        email: '',
        phone: '',
        realName: '',
        sex: '',
        age: ''
      },
      changePWDuserInfo: {
        userName: '',
        email: ''
      },
      topIn: '0px',
      topUp: '0px',
      topF: '0px',
      top: '5px',
      reback: false,
      pageType: 'login'
    }
  },
  methods: {
    initDom () {
      if (this.pageType === 'login') {
        this.topIn = (this.$refs.page.clientHeight - this.$refs.login.$el.clientHeight) / 2 + 'px'
      } else if (this.pageType === 'signUp') {
        this.topUp = (this.$refs.page.clientHeight - this.$refs.signUp.$el.clientHeight) / 2 + 'px'
      } else if (this.pageType === 'forgetPWD') {
        this.topF = (this.$refs.page.clientHeight - this.$refs.forgetPwd.$el.clientHeight) / 2 + 'px'
      }
    },
    // 注册返回按钮点击事件
    signUpClick () {
      if (!this.reback) {
        this.pageType = 'signUp'
        this.reback = true
        this.$emit("ChangeNum", "5")
      } else {
        this.pageType = 'login'
        this.reback = false
        this.$emit("ChangeNum", "3")
      }
    },
    // 登陆提交按钮点击事件
    signInClick () {
      if (this.pageType === 'login') {
        this.request({
          method: 'post',
          url: '/Login/Login',
          data: { 'userName': this.loginUserInfo.userName,
            'password': this.loginUserInfo.password },
          headers: {'Content-Type': 'application/json'}
        }).then((res) => {
          if (res.data.Status === '2000') {
            this.loginUser.id = res.data.Data.id
          } else {
            alert(res.data.Message)
          }
        }).catch(function (err) {
          console.info(err)
        })
      } else if (this.pageType === 'signUp') {
        if (this.signUpUserInfo.userName === '' ||
          this.signUpUserInfo.password === '' ||
          this.signUpUserInfo.enterPassword === '' ||
          this.signUpUserInfo.phone === '' ||
          this.signUpUserInfo.email === '' ||
          this.signUpUserInfo.realName === '' ||
          this.signUpUserInfo.sex === '' ||
          this.signUpUserInfo.age === '') {
          alert('有未填项')
        } else {
          this.request({
            method: 'post',
            url: '/Login/SignUp',
            data: { 'userName': this.signUpUserInfo.userName,
              'password': this.signUpUserInfo.password,
              'phone': this.signUpUserInfo.phone,
              'email': this.signUpUserInfo.email,
              'realName': this.signUpUserInfo.realName,
              'sex': this.signUpUserInfo.sex,
              'age': this.signUpUserInfo.age },
            headers: {'Content-Type': 'application/json'}
          }).then((res) => {
            if (res.data.Status === '2000') {
              alert('注册成功')
              this.pageType = 'login'
              this.reback = false
              this.$emit("ChangeNum", "3")
            } else {
              alert(res.data.Message)
            }
          }).catch(function (err) {
            console.info(err)
            alert('请求失败')
          })
        }
      } else if (this.pageType === 'forgetPWD') {
        this.request({
          method: 'post',
          url: '/Login/SignUp',
          data: { 'userName': this.signUpUserInfo.userName,
            'email': this.signUpUserInfo.email },
          headers: {'Content-Type': 'application/json'}
        }).then((res) => {
          if (res.data.Status === '2000') {
            alert('密码重置成功，新密码已发送至邮箱')
            this.pageType = 'login'
            this.reback = false
            this.$emit("ChangeNum", "3")
          } else {
            alert(res.data.Message)
          }
        }).catch(function (err) {
          console.info(err)
          alert('请求失败')
        })
      }
    },
    // 忘记密码点击事件
    changePassword () {
      this.pageType = 'forgetPWD'
      this.reback = true
      this.$emit("ChangeNum", "6")
    },
    onscroll () {
      let scrollTop = window.pageYOffset || document.documentElement.scrollTop || document.body.scrollTop
      if (scrollTop > 0) {
        this.top = (scrollTop + 10) + 'px'
      }
    }
  },
  mounted () {
    this.initDom()
    window.addEventListener('scroll', this.onscroll)
  },
  updated () {
    this.initDom()
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
#LoginPage{
  width: 100%;
}
.margin{
  margin: auto 10px;
  margin-top: 15px;
}
.inline{
  display: inline-block;
}
.login{
  width: 100%;
  text-align: center;
  margin: auto;
}
.signUp{
  width: 100%;
  text-align: center;
  margin: auto;
}
.forgetPwd{
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
.button{
  width: 100px;
  height: 40px;
  font-size: 18px;
  background-color: rgb(255,233,200);
  border: 1px solid #111;
  border-radius: 48%;
}
.a{
  font-size: 12px;
  width: 20px;
}
.back {
  width: 20px;
  height: 20px;
  position: absolute;
  z-index: 1;
  left: 5px;
}
.img {
  width: 20px;
  height: 20px;
}
</style>
