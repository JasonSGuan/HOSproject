<template>
  <div id="InputModel">
    <div class="divModel" ref="divPLogin">
      <div class="label" ref="divU">{{object.label}}:</div>
      <div class="divInput" :style="{ width: divWidth}">
        <input type="text" v-model="object.value" class="input" :style="{ width: inputWidth}" v-on:keyup="inputResponse" v-if="inputType" />
        <select v-model="object.value" v-else-if="isSelect" class="input" :style="{ width: inputWidth}" v-on:change="inputResponse">
          <option v-for="option in object.select" :key="option.value" :value="option.value">{{option.value}}</option>
        </select>
        <input type="password" v-model="object.value" class="input" :style="{ width: inputWidth}" v-on:keyup="inputResponse" v-else />
      </div>
      <div class="divContent" :style="{ width: inputWidth}" v-on:click="divResponse">{{object.content}}</div>
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
      divWidth: '0px',
      inputType: true,
      isSelect: false
    }
  },
  methods: {
    initDom () {
      if (this.object.label === '密码' || this.object.label === '确认密码') {
        this.inputType = false
        this.isSelect = false
      } else if (this.object.label === '性别') {
        this.isSelect = true
        this.inputType = false
      } else {
        this.isSelect = false
        this.inputType = true
      }
      this.divWidth = (this.$refs.divPLogin.clientWidth - this.$refs.divU.clientWidth - 8) + 'px'
      this.inputWidth = (this.$refs.divPLogin.clientWidth - this.$refs.divU.clientWidth - 8 - 30) + 'px'
    },
    divResponse () {
      if (this.object.vonType === 'divClick') {
        this.$emit('divResponse')
      }
    },
    inputResponse () {
      this.$emit('inputResponse', this.object.id, this.object.value)
    }
  },
  mounted () {
    this.initDom()
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
#InputModel{
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
  font-size: 12px;
  margin: auto;
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
  margin-left: 68px;
  margin-top: 5px;
  text-align: left;
}
</style>
