import VueRouter from 'vue-router'
import schemas from '../views/schemas/index'

const user = {
    template: '<div>User</div>'
  }
let app = 'Web/dist/';
let routes = [
    {
        path: '/',
        component: schemas
    },{
        path: '/home',
        component: user
    }
]
let router = new VueRouter({
    hashbang: false,
    mode: 'history',
    routes
})

export default router;