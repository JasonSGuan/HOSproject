<template>
  <div id="SignUp" ref="page">
    <div class="signUp">
      <div class="userName" ref="divPSignUp">
        <div class="lable" ref="divU">用户名：</div>
        <div class="divInput" :style="{ width: divWidth}">
          <input type="text" v-model="userName" class="input" :style="{ width: inputWidth}" />
        </div>
      </div>
      <div class="password">
        <div class="lable">密码：</div>
        <div class="divInput" :style="{ width: divWidth}">
          <input type="text" v-model="password" class="input" :style="{ width: inputWidth}" />
        </div>
      </div>
      <div class="password">
        <div class="lable">确认密码：</div>
        <div class="divInput" :style="{ width: divWidth}">
          <input type="text" v-model="password" class="input" :style="{ width: inputWidth}" />
        </div>
      </div>
      <div class="password">
        <div class="lable">手机号：</div>
        <div class="divInput" :style="{ width: divWidth}">
          <input type="text" v-model="password" class="input" :style="{ width: inputWidth}" />
        </div>
      </div>
      <div class="password">
        <div class="lable">邮箱：</div>
        <div class="divInput" :style="{ width: divWidth}">
          <input type="text" v-model="password" class="input" :style="{ width: inputWidth}" />
        </div>
      </div>
      <div class="password">
        <div class="lable">姓名：</div>
        <div class="divInput" :style="{ width: divWidth}">
          <input type="text" v-model="password" class="input" :style="{ width: inputWidth}" />
        </div>
      </div>
      <div class="password">
        <div class="lable">性别：</div>
        <div class="divInput" :style="{ width: divWidth}">
          <input type="text" v-model="password" class="input" :style="{ width: inputWidth}" />
        </div>
      </div>
      <div class="password">
        <div class="lable">年龄：</div>
        <div class="divInput" :style="{ width: divWidth}">
          <input type="text" v-model="password" class="input" :style="{ width: inputWidth}" />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'SignUp',
  data () {
    return {
      userName: '',
      password: '',
      inputWidth: '0px',
      divWidth: '0px',
      topUp: '0px'
    }
  },
  methods: {
    initDom () {
      this.divWidth = (this.$refs.divPSignUp.clientWidth - this.$refs.divU.clientWidth - 8) + 'px'
      this.inputWidth = (this.$refs.divPSignUp.clientWidth - this.$refs.divU.clientWidth - 8 - 60) + 'px'
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
.a{
  font-size: 12px;
  width: 20px;
}
</style>
