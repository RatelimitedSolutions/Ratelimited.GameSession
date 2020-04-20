import Vue from 'vue'
import App from './App.vue'
import Router from "vue-router";
import Home from "./views/Home.vue";
import Login from "./views/Login.vue";
import Register from "./views/Register.vue";


Vue.use(Router);

export const router = new Router({
  mode: "history",
  routes:[
    {
      path: "/",
      name: "home",
      component: Home
    },
    {
      path: "/home",
      component: Home
    },
    {
      path: "/login",
      component: Login
    },
    {
      path: "/register",
      component: Register
    },
    {
      path: "/private",
      name: "private",
      component: () => import("./views/private.vue")
    },
    {

    }
  ]
});