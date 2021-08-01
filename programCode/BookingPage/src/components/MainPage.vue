<template>
  <div id="MainPage" ref="page">
    <LoginPage ref="login" v-if="showType==='Login'" :style="{ 'margin-top': topIn }" v-on:ChangeNum="ChangeNum"></LoginPage>
    <Record v-else-if="showType==='Record'" v-bind:changeType="changeNum"></Record>
    <div class="buttonList" ref="buttonList" :style="{ 'top': top, 'left': left}">
      <table class="table">
        <tr>
          <td class="td">
            <button class="button" @click="addClick" :style="{ 'background-color': addColor }">记录</button>
          </td>
          <td class="td" style="border-left:1px solid black;border-right:1px solid black;">
            <button class="button" @click="billClick" :style="{ 'background-color': billColor }">账单</button>
          </td>
          <td class="td">
            <button class="button" @click="myClick" :style="{ 'background-color': myColor }">我的</button>
          </td>
        </tr>
      </table>
    </div>
  </div>
</template>

<script>
import LoginPage from './MyInfoModule/LoginModule/LoginPage.vue'
import Record from './RecordModule/Record.vue'
export default {
  components: { LoginPage, Record },
  name: 'MainPage',
  data () {
    return {
      top: '0px',
      left: '0px',
      topIn: '0px',
      billColor: 'rgb(225,255,230)',
      addColor: 'rgb(255,244,219)',
      myColor: 'rgb(225,255,230)',
      showType: 'Record',
      changeNum: '0',
      loginUserId: {
        id: ''
      }
    }
  },
  methods: {
    initDom () {
      this.top = (this.$refs.page.clientHeight - this.$refs.buttonList.clientHeight) + 'px'
      this.left = (this.$refs.page.clientWidth - this.$refs.buttonList.clientWidth) / 2 + 'px'
    },
    setPosition () {
      if (this.showType === 'Login') {
        this.topIn = (this.$refs.page.clientHeight - this.$refs.login.$el.clientHeight) / 2 + 'px'
      }
    },
    myClick () {
      this.billColor = 'rgb(225,255,230)'
      this.addColor = 'rgb(225,255,230)'
      this.myColor = 'rgb(255,244,219)'
      if (this.loginUserId.id === '') {
        this.showType = 'Login'
        this.changeNum = '3'
      } else {
        this.showType = 'MyInfo'
        this.changeNum = '4'
      }
    },
    addClick () {
      this.billColor = 'rgb(225,255,230)'
      this.addColor = 'rgb(255,244,219)'
      this.myColor = 'rgb(225,255,230)'
      this.showType = 'Record'
      this.changeNum = '1'
    },
    billClick () {
      this.billColor = 'rgb(255,244,219)'
      this.addColor = 'rgb(225,255,230)'
      this.myColor = 'rgb(225,255,230)'
      this.showType = 'Bill'
      this.changeNum = '2'
    },
    onscroll () {
      let scrollTop = window.pageYOffset || document.documentElement.scrollTop || document.body.scrollTop
      if (scrollTop > 0) {
        this.$emit('scroll', scrollTop)
        this.top = (this.$refs.page.clientHeight - this.$refs.buttonList.clientHeight + scrollTop) + 'px'
      }
    },
    ChangeNum (value) {
      this.changeNum = value
      this.setPosition()
    }
  },
  mounted () {
    this.initDom()
    window.addEventListener('scroll', this.onscroll)
  },
  updated () {
    this.setPosition()
  },
  created () {
  }
}
</script>

<style scoped>
#MainPage {
  width: 100%;
  height: 100%;
}
.buttonList {
  width: 99%;
  text-align: center;
  margin: auto;
  position: absolute;
  z-index: 1;
}
.button {
  width: 100%;
  height: 30px;
  opacity: 0.75;
  background-color: rgb(225,255,230);
  border: none;
  font-size: 18px;
  display: inline-block;
}
.td {
  width: 33%;
  height: 30px;
}
.table {
  width: 100%;
  height: 30px;
  border-collapse: collapse;
}
</style>
