import Vue from 'vue'
import VueRouter from 'vue-router'
import axios from 'axios'
import ElementUI from 'element-ui'
import 'element-ui/lib/theme-chalk/index.css'
import App from './App.vue'
import router from './router/router'

Vue.use(ElementUI)
Vue.use(VueRouter) 
axios.defaults.baseURL= localStorage.getItem("apiServer")
Vue.prototype.axios = axios;

new Vue({
  el: '#app',
  router,
  render: h => h(App)
})
