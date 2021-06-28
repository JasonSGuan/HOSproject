import Vue from 'vue'
import Router from 'vue-router'
import LoginPage from '@/components/LoginPage'
import Login from '@/components/Login/Login'
import SignUp from '@/components/Login/SignUp'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/Login',
      name: 'Login',
      component: Login
    },
    {
      path: '/Login',
      name: 'SignUp',
      component: SignUp
    },
    {
      path: '/',
      name: 'LoginPage',
      component: LoginPage
    }
  ]
})
