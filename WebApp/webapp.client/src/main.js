import { createApp } from 'vue'
import { createRouter, createWebHistory } from 'vue-router'
import App from './App.vue';

import 'vuetify/styles'
import { createVuetify } from 'vuetify'
import * as components from 'vuetify/components'
import * as directives from 'vuetify/directives'

import EmployeesView from '@/components/EmployeesView.vue'

const vuetify = createVuetify({
    components,
    directives
})

const routes = [
    { path: '/', name: 'App', component: App, redirect: '/employees' },
    { path: '/employees', name: 'employees', component: EmployeesView },
];

const router = createRouter({
    history: createWebHistory(),
    routes: routes,
})

createApp(App).use(router, vuetify).mount('#app')