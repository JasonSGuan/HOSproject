<template>
  <div id="Record" ref="record">
   <!--  <div class="back" @click="backClick">
      <img src="~@/assets/返回.png" class="img" />
    </div> -->
    <div class="div" ref="docker" :style="{ 'margin-top': top }">
        <li><ul>1</ul><ul>2</ul>
        </li>
    </div>
  </div>
</template>

<script>
export default {
  name: 'Record',
  data () {
    return {
      top: '0px'
    }
  },
  methods: {
    initDom () {
      this.top = (this.$refs.record.clientHeight - this.$refs.docker.clientHeight - 40) + 'px'
    }
  },
  mounted () {
    this.initDom()
  },
  created () {
    this.request.get('http://localhost:8080/static/record.json').then((response)=>{
      console.info(response.data.data)
    })
  }
}
</script>

<style scoped>
#Record {
  height: 100%;
  width: 100%;
  text-align: center;
}
.div {
  height: 90%;
  width: 99%;
  border: 1px solid black;
  background-color: white;
  opacity: 50%;
  border-radius: 5%;
}
</style>
