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
          path: 'Balances',
          name: 'Баланс товаров на складах',
          component: () => import('../views/entities/BalancesView.vue'),
        },
        {
          path: 'Goods',
          name: 'Товары',
          component: () => import('../views/entities/GoodsView.vue'),
        },
        {
          path: 'Invoices',
          name: 'Накладные',
          component: () => import('../views/entities/InvoicesView.vue'),
        },
        {
          path: 'Positions',
          name: 'Должности',
          component: () => import('../views/entities/PositionsView.vue'),
        },
        {
          path: 'Staff',
          name: 'Персонал',
          component: () => import('../views/entities/StaffView.vue'),
        },
        {
          path: 'Warehouses',
          name: 'Склады',
          component: () => import('../views/entities/WarehousesView.vue'),
        },
      ],
    },
    {
      path: '/Reports/',
      children: [
        {
          path: 'ReorderGoodsReport',
          name: 'Отчёт по товарам, которые скоро закончатся',
          component: () => import('../views/reports/ReorderGoodsReportView.vue'),
        },
        {
          path: 'StaffPerformanceReport',
          name: 'Отчёт по производительности персонала',
          component: () => import('../views/reports/StaffPerformanceReportView.vue'),
        },
        {
          path: 'WarehousePeriodReport',
          name: 'Отчёт по складам за период',
          component: () => import('../views/reports/WarehousePeriodReportView.vue'),
        },
      ],
    },
  ],
})

export default router
