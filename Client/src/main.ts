import { createApp } from 'vue'
import PrimeVue from 'primevue/config'

import App from './App.vue'
import router from './router'
import Material from '@primevue/themes/material'

const app = createApp(App)

app.use(PrimeVue, {
  theme: {
    preset: Material,
    options: {
      darkModeSelector: '.my-app-dark',
    },
  },
})
app.use(router)

app.mount('#app')
