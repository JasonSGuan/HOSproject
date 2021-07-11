// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import App from './App'
import axios from 'axios'
import router from './router'

Vue.config.productionTip = false
// 全局axios
Vue.prototype.request = axios
axios.defaults.baseURL = 'http://www.lightor.vip/BookingApi/api'
// 路由
Vue.prototype.rou = router

/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  axios,
  components: { App },
  template: '<App/>'
})
