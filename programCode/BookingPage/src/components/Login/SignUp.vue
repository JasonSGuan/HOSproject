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
        // 用户名
        this.userInfo.userName = value
        let reg = /^[a-zA-Z]{1,20}$/
        let pattern = new RegExp(reg)
        if (!pattern.test(value)) {
          this.userInfoList[0].content = '用户名只能有字母组成，长度不超过20位'
        } else {
          this.request({
            method: 'post',
            url: '/Login/Repeated',
            data: { 'userName': this.userInfo.userName },
            headers: {'Content-Type': 'application/json'}
          }).then((res) => {
            if (res.data.Status !== '2000') {
              this.userInfoList[0].content = '用户名已存在'
            } else {
              this.userInfoList[0].content = ''
            }
          }).catch(function (err) {
            console.info(err)
          })
        }
      } else if (id === 2) {
        // 密码
        this.userInfo.password = value
        let reg = /^[a-zA-Z]\w{5,17}$/
        let pattern = new RegExp(reg)
        if (value === '') {
          this.userInfoList[1].content = '密码不能为空'
        } else {
          if (!pattern.test(value)) {
            this.userInfoList[1].content = '密码必须由字符、数字、下划线组成6-18位'
          } else {
            this.userInfoList[1].content = ''
          }
        }
        if (this.userInfo.enterPassword !== '') {
          if (this.userInfo.password !== this.userInfo.enterPassword) {
            this.userInfoList[2].content = '两次密码不一样'
          } else {
            this.userInfoList[2].content = ''
          }
        }
      } else if (id === 3) {
        // 确认密码
        this.userInfo.enterPassword = value
        if (this.userInfo.password !== this.userInfo.enterPassword) {
          this.userInfoList[2].content = '两次密码不一样'
        } else {
          this.userInfoList[2].content = ''
        }
      } else if (id === 4) {
        // 手机号
        this.userInfo.phone = value
        let reg = /^\d{11}$/
        let pattern = new RegExp(reg)
        if (!pattern.test(value)) {
          this.userInfoList[3].content = '手机号格式不合法'
        } else {
          this.userInfoList[3].content = ''
        }
      } else if (id === 5) {
        // 邮箱
        this.userInfo.email = value
        let reg = /^([a-zA-Z0-9]+[_|_|.]?)*[a-zA-Z0-9]+@([a-zA-Z0-9]+[_|_|.]?)*[a-zA-Z0-9]+\.[a-zA-Z]{2,3}$/
        let pattern = new RegExp(reg)
        if (!pattern.test(value)) {
          this.userInfoList[4].content = '邮箱格式不合法'
        } else {
          this.userInfoList[4].content = ''
        }
      } else if (id === 6) {
        // 姓名
        this.userInfo.realName = value
        let reg = /^[\u4E00-\u9FA5]{2,4}$/
        let pattern = new RegExp(reg)
        if (!pattern.test(value)) {
          this.userInfoList[5].content = '姓名不合法'
        } else {
          this.userInfoList[5].content = ''
        }
      } else if (id === 7) {
        // 性别
        this.userInfo.sex = value
      } else {
        // 年龄
        this.userInfo.age = value
        let reg = /^\d{0,3}$/
        let pattern = new RegExp(reg)
        if (!pattern.test(value)) {
          this.userInfoList[7].content = '年龄不合法'
        } else {
          this.userInfoList[7].content = ''
        }
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
