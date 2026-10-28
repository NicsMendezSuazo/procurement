import { createRouter, createWebHistory } from 'vue-router'
import type { RouteRecordRaw } from 'vue-router'

const routes: RouteRecordRaw[] = [
  {
    path: '/',
    name: 'dashboard',
    component: () => import('../../modules/dashboard/presentation/views/DashboardView.vue'),
  },
  {
    path: '/purchase-requests',
    name: 'purchase-requests',
    component: () => import('../../modules/purchase-requests/presentation/views/PurchaseRequestListView.vue'),
  },
  {
    path: '/purchase-orders',
    name: 'purchase-orders',
    component: () => import('../../modules/purchase-orders/presentation/views/PurchaseOrderListView.vue'),
  },
  {
    path: '/suppliers',
    name: 'suppliers',
    component: () => import('../../modules/suppliers/presentation/views/SupplierListView.vue'),
  },
  {
    path: '/settings',
    name: 'settings',
    component: () => import('../../modules/settings/presentation/views/SettingsView.vue'),
  },
]

export const router = createRouter({
  history: createWebHistory(),
  routes,
})
