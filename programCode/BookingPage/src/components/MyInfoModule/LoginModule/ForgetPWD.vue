<template>
  <div id='ForgetPWD'>
    <div class="ForgetPW">
      <InputModel v-for="user in userInfoList" v-bind:key="user.id" v-bind:object="user" v-on:inputResponse="inputValue">
      </InputModel>
    </div>
  </div>
</template>

<script>
import InputModel from '../../InputModel.vue'
export default {
  props: ['userInfo'],
  name: 'ForgetPWD',
  components: { InputModel },
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
        label: '邮箱',
        value: '',
        content: '',
        vonType: ''
      }]
    }
  },
  methods: {
    inputValue (id, value) {
      if (id === 1) {
        this.userInfo.userName = value
        let reg = /^[a-zA-Z]{5,20}$/
        let pattern = new RegExp(reg)
        if (!pattern.test(value)) {
          this.userInfoList[0].content = '用户名只能由字母组成，长度不超过5-20位'
        } else {
          this.request({
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
            console.info(err)
            alert('请求失败')
          })
        }
      } else {
        this.userInfo.email = value
        let reg = /^([a-zA-Z0-9]+[_|_|.]?)*[a-zA-Z0-9]+@([a-zA-Z0-9]+[_|_|.]?)*[a-zA-Z0-9]+\.[a-zA-Z]{2,3}$/
        let pattern = new RegExp(reg)
        if (!pattern.test(value)) {
          this.userInfoList[1].content = '邮箱格式不合法'
        } else {
          this.userInfoList[1].content = ''
        }
      }
    }
  }
}
</script>

<style scoped>
.ForgetPW {
  width: 100%;
  text-align: center;
  margin: auto;
}
</style>
