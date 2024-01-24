import { createApp } from 'vue'
import { createRouter, createWebHistory } from 'vue-router'
import App from './App.vue';

import 'vuetify/styles'
import 'vuetify/dist/vuetify.min.css'
import { createVuetify } from 'vuetify'
import * as components from 'vuetify/components'
import * as directives from 'vuetify/directives'
import '@/assets/styles.css'
import '@mdi/font/css/materialdesignicons.css'

import EmployeesView from '@/views/EmployeesView.vue'
import CompaniesView from '@/views/CompaniesView.vue'
import ProjectsView from '@/views/ProjectsView.vue'

const vuetify = createVuetify({
    components,
    directives,
    theme: {
        defaultTheme: 'light',
    }
})

const routes = [
    { path: '/', name: 'App', component: App, redirect: '/companies' },
    { path: '/employees', name: 'employees', component: EmployeesView },
    { path: '/companies', name: 'companies', component: CompaniesView },
    { path: '/projects', name: 'projects', component: ProjectsView },
];

const router = createRouter({
    history: createWebHistory(),
    routes: routes,
})

createApp(App).use(vuetify).use(router).mount('#app')