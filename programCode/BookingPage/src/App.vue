<template>
  <div id="app">
    <div class="background" :style="{ top: topBg}">
      <img :src="imgUrl" class="bgImg" />
    </div>
    <router-view class="other" v-on:scroll="fixedBgImg"/>
  </div>
</template>

<script>
export default {
  name: 'App',
  data () {
    return {
      imgUrl: require('./assets/卡通风.png'),
      topBg: '0px'
    }
  },
  methods: {
    fixedBgImg (value) {
      this.topBg = value + 'px'
    }
  }
}
</script>

<style>
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}
.bgImg{
  width: 100%;
  height: 100%;
  background-attachment: fixed;
}
.background{
  z-index: -1;
  width: 100%;
  height: 100%;
  position: absolute;
  top: 0px;
  left: 0px;
}
.other{
  z-index: 1;
  position: absolute;
  top: 0px;
  left: 0px;
}
</style>
