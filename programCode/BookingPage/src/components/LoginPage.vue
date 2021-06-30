<template>
    <div id="loginPage" class="loginPage" ref="page">
      <Login class="login" ref="login" v-bind:userInfo="loginUserInfo" :style="{ 'margin-top': topIn}" v-if="isLogin">
      </Login>
      <SignUp class="signUp" ref="signUp" v-bind:userInfo="signUpUserInfo" :style="{ 'margin-top': topUp}" v-else>
      </SignUp>
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
import Login from './Login/Login.vue'
import SignUp from './Login/SignUp.vue'
export default {
  components: { Login, SignUp },
  name: 'loginPage',
  data () {
    return {
      loginUserInfo: [ {
        id: 1,
        label: '用户名',
        value: '',
        content: ''
      }, {
        id: 2,
        label: '密码',
        value: '',
        content: '忘记密码'
      }
      ],
      signUpUserInfo: {
        userName: '',
        password: '',
        EnterPassword: '',
        email: '',
        phone: '',
        realName: '',
        sex: '',
        age: ''
      },
      topIn: '0px',
      topUp: '0px',
      reback: false,
      isLogin: true
    }
  },
  methods: {
    initDom () {
      if (this.isLogin) {
        this.topIn = (this.$refs.page.clientHeight - this.$refs.login.$el.clientHeight) / 2 + 'px'
      } else {
        this.topUp = (this.$refs.page.clientHeight - this.$refs.signUp.$el.clientHeight) / 2 + 'px'
      }
    },
    signUpClick () {
      if (!this.reback) {
        this.isLogin = false
        this.reback = true
      } else {
        this.isLogin = true
        this.reback = false
      }
    },
    signInClick () {
      console.info(this.loginUserInfo[0].value)
      console.info(this.loginUserInfo[1].value)
      this.$axios({
        method:'post',
        url:'',
        data:'',
      }).then(function(res){
        console.info(res)
      }).catch(function(err){
        console.info(err)
      })
    }
  },
  mounted () {
    this.initDom()
  },
  updated () {
    this.initDom()
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.loginPage{
  width: 100%;
  height: 100%;
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
</style>
