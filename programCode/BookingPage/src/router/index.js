import Vue from 'vue'
import Router from 'vue-router'
import LoginPage from '@/components/MyInfoModule/LoginModule/LoginPage'
import Login from '@/components/MyInfoModule/LoginModule/Login'
import SignUp from '@/components/MyInfoModule/LoginModule/SignUp'
import ForgetPWD from '@/components/MyInfoModule/LoginModule/ForgetPWD'
import InputModel from '@/components/InputModel'
import Record from '@/components/RecordModule/Record'
import MainPage from '@/components/MainPage'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/MyInfoModule/LoginModule',
      name: 'Login',
      component: Login
    },
    {
      path: '/MyInfoModule/LoginModule',
      name: 'SignUp',
      component: SignUp
    },
    {
      path: '/MyInfoModule/LoginModule',
      name: 'ForgetPWD',
      component: ForgetPWD
    },
    {
      path: '/MyInfoModule/LoginModule',
      name: 'LoginPage',
      component: LoginPage
    },
    {
      path: '/',
      name: 'InputModel',
      component: InputModel
    },
    {
      path: '/RecordModule',
      name: 'Record',
      component: Record
    },
    {
      path: '/',
      name: 'MainPage',
      component: MainPage
    }
  ]
})
