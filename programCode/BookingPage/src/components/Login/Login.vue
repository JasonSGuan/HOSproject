<template>
  <div id="Login">
    <div class="login">
      <InputModel v-for="user in userInfo" v-bind:key="user.id" v-bind:object="user">
      </InputModel>
      <!-- <div class="userName" ref="divPLogin">
        <div class="lable" ref="divU">用户名：</div>
        <div class="divInput" :style="{ width: divWidth}">
          <input type="text" v-model="userInfo.userName" class="input" :style="{ width: inputWidth}" />
        </div>
      </div>
      <div class="password">
        <div class="lable">密码：</div>
        <div class="divInput" :style="{ width: divWidth}">
          <input type="text" v-model="userInfo.password" class="input" :style="{ width: inputWidth}" />
          <a @click="forgetPWDClick" class="a">忘记密码</a>
        </div>
      </div> -->
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
      inputWidth: '0px',
      divWidth: '0px'
    }
  },
  methods: {
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
