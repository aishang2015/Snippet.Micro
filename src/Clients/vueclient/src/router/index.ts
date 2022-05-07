import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import LoginView from '../views/login/LoginView.vue'
import LayoutView from '../views/layout/LayoutView.vue'

const routes: Array<RouteRecordRaw> = [
  {
    path: '/',
    name: 'layout',
    component: LayoutView,
    
    children: [
      {
        path: '/',
        name: 'home',
        component: () => import('../views/HomeView.vue')
      },
      {
        path: '/about',
        name: 'about',
        component: () => import('../views/AboutView.vue')
      },
      {
        path: '/api',
        name: 'api',
        component: () => import('../views/api/api.vue')
      }
    ]
  },
  {
    path: '/login',
    name: 'login',
    component: LoginView
  },
  {
    path: '/:pathMatch(.*)*',
    name: 'notfound',
    component: () => import(/* webpackChunkName: "about" */ '../views/not-found/NotFoundView.vue')
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

router.beforeEach((to, from, next) => {

  const isLogin = localStorage.getItem("token") !== null &&
    localStorage.getItem("token") !== undefined &&
    localStorage.getItem("token") !== "";

  if (!isLogin && to.name !== "login") {
    next({ name: "login" });
  }  
  else if (isLogin && to.name === "login") {
    next({ name: "home" });
  } else {
    next();
  }
})

export default router
