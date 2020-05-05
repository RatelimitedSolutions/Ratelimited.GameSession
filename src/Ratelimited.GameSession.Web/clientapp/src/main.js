import { library } from '@fortawesome/fontawesome-svg-core';
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
import {
  faHome,
  faUser,
  faUserPlus,
  faSignInAlt,
  faSignOutAlt
} from '@fortawesome/free-solid-svg-icons';

import Vue from 'vue';

import 'bootstrap';
import 'bootstrap/dist/css/bootstrap.min.css';

import Vuex from 'vuex';

import * as VeeValidate from 'vee-validate';

import App from './App.vue';
import { router } from './router';
import store from './store';


Vue.component('font-awesome-icon', FontAwesomeIcon);
library.add(faHome, faUser, faUserPlus, faSignInAlt, faSignOutAlt);

Vue.use(Vuex);
Vue.use(VeeValidate);

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app');
