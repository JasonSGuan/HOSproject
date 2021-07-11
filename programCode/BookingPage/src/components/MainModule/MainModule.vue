<template>
  <div id="MainModule" ref="page">
    <div class="profile" @click="headClick">
    </div>
    <div class="buttonList" ref="buttonList" :style="{ 'margin-top': top}">
      <hr class="line" />
      <button class="button" @click="addClick">添加记录</button>
      <hr class="line" />
      <button class="button" @click="familyClick">家庭管理</button>
      <hr class="line" />
      <button class="button" @click="billClick">查看账单</button>
      <hr class="line" />
    </div>
  </div>
</template>

<script>
export default {
  props: ['loginUser'],
  name: 'MainModule',
  data () {
    return {
      top: '0px'
    }
  },
  methods: {
    initDom () {
      this.top = (this.$refs.page.clientHeight - this.$refs.buttonList.clientHeight) / 2 + 'px'
    },
    headClick () {
      this.$emit('buttonClick', '0')
    },
    addClick () {
      this.$emit('buttonClick', '1')
    },
    familyClick () {
      this.$emit('buttonClick', '2')
    },
    billClick () {
      this.$emit('buttonClick', '3')
    }
  },
  mounted () {
    this.initDom()
  }
}
</script>

<style scoped>
#MainModule {
  width: 100%;
  height: 100%;
}
.profile {
  width: 30px;
  height: 30px;
  border-radius: 50%;
  background-color: black;
  position: absolute;
  z-index: -1;
  top: 10px;
  left: 10px;
}
.buttonList {
  width: 100%;
  text-align: center;
  margin: auto;
}
.button {
  width: 80%;
  height: 40px;
  opacity: 0.75;
  background-color: rgb(225,255,230);
  border: none;
  font-size: 18px;
}
.line {
  width: 70%;
  height: 1px;
  margin: 0px auto;
  background-color: gray;
  border: none;
}
</style>
