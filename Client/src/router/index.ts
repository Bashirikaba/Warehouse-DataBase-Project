import { createRouter, createWebHistory } from 'vue-router'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'Home',
      component: () => import('../views/HomeView.vue'),
    },
    {
      path: '/Entities/',
      children: [
        {
          path: 'Positions',
          name: 'Positions',
          component: () => import('../views/PositionsView.vue'),
        },
      ],
    },
  ],
})

export default router
