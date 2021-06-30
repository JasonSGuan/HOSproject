<template>
  <div id="InputModel" class="model">
    <div class="divModel" ref="divPLogin">
      <div class="label" ref="divU">{{object.label}}:</div>
      <div class="divInput" :style="{ width: divWidth}">
        <input type="text" v-model="object.value" class="input" :style="{ width: inputWidth}" />
        <div class="divContent">{{object.content}}</div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: ['object'],
  name: 'InputModel',
  data () {
    return {
      inputWidth: '0px',
      divWidth: '0px'
    }
  },
  methods: {
    initDom () {
      this.divWidth = (this.$refs.divPLogin.clientWidth - this.$refs.divU.clientWidth - 8) + 'px'
      this.inputWidth = (this.$refs.divPLogin.clientWidth - this.$refs.divU.clientWidth - 8 - 60) + 'px'
    }
  },
  mounted () {
    this.initDom()
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.model{
  width: 100%;
  text-align: center;
  margin: auto;
}
.divModel{
  width: 90%;
  margin: auto;
  margin-top: 15px;
}
.label{
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
.divContent{
  font-size: 12px;
  width: 50px;
  display: inline-block;
}
</style>
