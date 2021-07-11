import Vue from 'vue'
import Router from 'vue-router'
import LoginPage from '@/components/LoginModule/LoginPage'
import Login from '@/components/LoginModule/Login'
import SignUp from '@/components/LoginModule/SignUp'
import InputModel from '@/components/LoginModule/InputModel'
import Record from '@/components/RecordModule/Record'
import MainPage from '@/components/MainPage'
import MainModule from '@/components/MainModule/MainModule'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/LoginModule',
      name: 'Login',
      component: Login
    },
    {
      path: '/LoginModule',
      name: 'SignUp',
      component: SignUp
    },
    {
      path: '/LoginModule',
      name: 'LoginPage',
      component: LoginPage
    },
    {
      path: '/LoginModule',
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
    },
    {
      path: '/MainModule',
      name: 'MainModule',
      component: MainModule
    }
  ]
})
