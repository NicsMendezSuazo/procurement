<script setup lang="ts">
import { ref, computed, onMounted } from 'vue'
import {
  LayoutDashboard,
  CheckCircle2,
  Clock,
  Wallet,
  ShoppingCart,
  FileText,
  Users,
  TrendingUp,
  Loader2,
  ArrowRight,
} from 'lucide-vue-next'
import { RouterLink } from 'vue-router'
import AdminLayout from '../../../../shared/layouts/AdminLayout.vue'
import { formatCurrency, formatDate } from '../../../../core/utils/formatters'
import { POStatus, POStatusLabels, ModeOfProcurementLabels } from '../../../purchase-orders/domain/enums/POStatus'
import type { PurchaseOrder } from '../../../purchase-orders/domain/models/PurchaseOrder'
import { GetPurchaseOrdersUseCase } from '../../../purchase-orders/application/usecases/GetPurchaseOrdersUseCase'
import { MockPurchaseOrderRepository } from '../../../purchase-orders/infrastructure/repositories/MockPurchaseOrderRepository'

const orders = ref<PurchaseOrder[]>([])
const isLoading = ref(true)

const repository = new MockPurchaseOrderRepository()
const getUseCase = new GetPurchaseOrdersUseCase(repository)

onMounted(async () => {
  orders.value = await getUseCase.execute()
  isLoading.value = false
})

const metrics = computed(() => {
  const approved = orders.value.filter((o) => o.status === POStatus.Approved)
  const pending = orders.value.filter((o) => o.status === POStatus.PendingBAC)
  const draft = orders.value.filter((o) => o.status === POStatus.Draft)
  const delivered = orders.value.filter((o) => o.status === POStatus.Delivered)
  const totalBudget = approved.reduce((s, o) => s + o.totalAmount, 0)
  const pendingValue = pending.reduce((s, o) => s + o.totalAmount, 0)
  return {
    totalOrders: orders.value.length,
    approved: approved.length,
    pending: pending.length,
    draft: draft.length,
    delivered: delivered.length,
    totalBudget,
    pendingValue,
  }
})

const recentOrders = computed(() => orders.value.slice(0, 5))

const statusDistribution = computed(() => {
  const statuses: POStatus[] = Object.values(POStatus)
  return statuses.map((status: POStatus) => ({
    status,
    label: POStatusLabels[status],
    count: orders.value.filter((o: PurchaseOrder) => o.status === status).length,
    pct: orders.value.length ? Math.round((orders.value.filter((o: PurchaseOrder) => o.status === status).length / orders.value.length) * 100) : 0,
  }))
})

const statusColors: Record<POStatus, string> = {
  [POStatus.Draft]: 'bg-slate-400',
  [POStatus.PendingBAC]: 'bg-amber-500',
  [POStatus.Approved]: 'bg-emerald-500',
  [POStatus.Delivered]: 'bg-blue-500',
  [POStatus.Cancelled]: 'bg-red-500',
}

const kpiCards = computed(() => [
  { label: 'Total Purchase Orders', value: metrics.value.totalOrders.toString(), sub: 'All time', icon: ShoppingCart, color: 'brand' },
  { label: 'Approved POs', value: metrics.value.approved.toString(), sub: `${metrics.value.delivered} delivered`, icon: CheckCircle2, color: 'emerald' },
  { label: 'Pending BAC Review', value: metrics.value.pending.toString(), sub: 'Awaiting action', icon: Clock, color: 'amber' },
  { label: 'Budget Allocated', value: formatCurrency(metrics.value.totalBudget), sub: 'Approved value', icon: Wallet, color: 'brand' },
])

const colorClasses: Record<string, { bg: string; text: string; ring: string }> = {
  emerald: { bg: 'bg-emerald-50', text: 'text-emerald-600', ring: 'ring-emerald-100' },
  amber: { bg: 'bg-amber-50', text: 'text-amber-600', ring: 'ring-amber-100' },
  brand: { bg: 'bg-brand-50', text: 'text-brand-600', ring: 'ring-brand-100' },
}

const quickLinks = [
  { label: 'Purchase Orders', desc: 'Manage and approve POs', icon: ShoppingCart, to: '/purchase-orders' },
  { label: 'Purchase Requests', desc: 'View incoming PRs', icon: FileText, to: '/purchase-requests' },
  { label: 'Suppliers', desc: 'Registered vendor directory', icon: Users, to: '/suppliers' },
]
</script>

<template>
  <AdminLayout>
    <div class="max-w-7xl mx-auto">
      <div class="mb-6">
        <div class="flex items-center gap-2 text-sm text-slate-500 mb-1">
          <LayoutDashboard :size="14" />
          <span class="text-slate-700 font-medium">Dashboard</span>
        </div>
        <h1 class="text-2xl font-bold text-slate-900">Procurement Overview</h1>
        <p class="text-sm text-slate-500 mt-1">Davao Regional Medical Center — BAC Secretariat Portal</p>
      </div>

      <div v-if="isLoading" class="flex items-center justify-center py-20">
        <Loader2 :size="32" class="animate-spin text-brand-500" />
      </div>

      <template v-else>
        <div class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-4 mb-6">
          <div v-for="card in kpiCards" :key="card.label" class="bg-white rounded-xl border border-slate-200 p-5 shadow-sm hover:shadow-md transition-shadow">
            <div class="flex items-start justify-between">
              <div>
                <p class="text-sm font-medium text-slate-500">{{ card.label }}</p>
                <p class="text-2xl font-bold text-slate-900 mt-1 tabular-nums">{{ card.value }}</p>
                <p class="text-xs text-slate-400 mt-1">{{ card.sub }}</p>
              </div>
              <div :class="['flex items-center justify-center w-11 h-11 rounded-lg ring-8', colorClasses[card.color].bg, colorClasses[card.color].text, colorClasses[card.color].ring]">
                <component :is="card.icon" :size="22" />
              </div>
            </div>
          </div>
        </div>

        <div class="grid grid-cols-1 lg:grid-cols-3 gap-6 mb-6">
          <div class="bg-white rounded-xl border border-slate-200 p-5 shadow-sm">
            <h2 class="text-base font-semibold text-slate-800 mb-4">Status Distribution</h2>
            <div class="space-y-3">
              <div v-for="item in statusDistribution" :key="item.status">
                <div class="flex items-center justify-between text-sm mb-1">
                  <span class="text-slate-600">{{ item.label }}</span>
                  <span class="text-slate-400 font-medium tabular-nums">{{ item.count }} ({{ item.pct }}%)</span>
                </div>
                <div class="h-2 bg-slate-100 rounded-full overflow-hidden">
                  <div :class="['h-full rounded-full transition-all duration-500', statusColors[item.status]]" :style="{ width: item.pct + '%' }" />
                </div>
              </div>
            </div>
          </div>

          <div class="bg-white rounded-xl border border-slate-200 p-5 shadow-sm">
            <h2 class="text-base font-semibold text-slate-800 mb-4">Pending BAC Value</h2>
            <div class="flex items-center gap-3 mb-4">
              <div class="flex items-center justify-center w-12 h-12 rounded-lg bg-amber-50 text-amber-600 ring-8 ring-amber-100">
                <Clock :size="24" />
              </div>
              <div>
                <p class="text-2xl font-bold text-slate-900 tabular-nums">{{ formatCurrency(metrics.pendingValue) }}</p>
                <p class="text-xs text-slate-400">{{ metrics.pending }} orders awaiting review</p>
              </div>
            </div>
            <div class="flex items-center gap-2 text-sm">
              <TrendingUp :size="14" class="text-emerald-500" />
              <span class="text-slate-600">{{ metrics.draft }} drafts in queue</span>
            </div>
          </div>

          <div class="bg-white rounded-xl border border-slate-200 p-5 shadow-sm">
            <h2 class="text-base font-semibold text-slate-800 mb-4">Quick Actions</h2>
            <div class="space-y-2">
              <RouterLink v-for="link in quickLinks" :key="link.label" :to="link.to" class="flex items-center gap-3 p-3 rounded-lg border border-slate-200 hover:border-brand-300 hover:bg-brand-50 transition-colors group">
                <div class="flex items-center justify-center w-9 h-9 rounded-lg bg-slate-100 text-slate-600 group-hover:bg-brand-100 group-hover:text-brand-600 transition-colors">
                  <component :is="link.icon" :size="18" />
                </div>
                <div class="flex-1 min-w-0">
                  <p class="text-sm font-semibold text-slate-700">{{ link.label }}</p>
                  <p class="text-xs text-slate-400 truncate">{{ link.desc }}</p>
                </div>
                <ArrowRight :size="16" class="text-slate-300 group-hover:text-brand-500 transition-colors" />
              </RouterLink>
            </div>
          </div>
        </div>

        <div class="bg-white rounded-xl border border-slate-200 shadow-sm overflow-hidden">
          <div class="px-5 py-4 border-b border-slate-200 flex items-center justify-between">
            <h2 class="text-base font-semibold text-slate-800">Recent Purchase Orders</h2>
            <RouterLink to="/purchase-orders" class="text-sm font-medium text-brand-600 hover:text-brand-700 flex items-center gap-1">
              View All <ArrowRight :size="14" />
            </RouterLink>
          </div>
          <div class="overflow-x-auto">
            <table class="w-full text-sm">
              <thead class="bg-slate-50 border-b border-slate-200">
                <tr>
                  <th class="text-left py-3 px-4 font-semibold text-slate-600 text-xs uppercase tracking-wider">PO Number</th>
                  <th class="text-left py-3 px-4 font-semibold text-slate-600 text-xs uppercase tracking-wider">Supplier</th>
                  <th class="text-left py-3 px-4 font-semibold text-slate-600 text-xs uppercase tracking-wider hidden md:table-cell">Mode</th>
                  <th class="text-right py-3 px-4 font-semibold text-slate-600 text-xs uppercase tracking-wider">Amount</th>
                  <th class="text-left py-3 px-4 font-semibold text-slate-600 text-xs uppercase tracking-wider hidden sm:table-cell">Date</th>
                </tr>
              </thead>
              <tbody class="divide-y divide-slate-100">
                <tr v-for="order in recentOrders" :key="order.id" class="hover:bg-slate-50 transition-colors">
                  <td class="py-3 px-4"><span class="font-semibold text-brand-700">{{ order.poNumber }}</span></td>
                  <td class="py-3 px-4 text-slate-700">{{ order.supplierName }}</td>
                  <td class="py-3 px-4 text-slate-500 hidden md:table-cell">{{ ModeOfProcurementLabels[order.modeOfProcurement] }}</td>
                  <td class="py-3 px-4 text-right font-semibold text-slate-900 tabular-nums">{{ formatCurrency(order.totalAmount) }}</td>
                  <td class="py-3 px-4 text-slate-500 hidden sm:table-cell">{{ formatDate(order.createdAt) }}</td>
                </tr>
              </tbody>
            </table>
          </div>
        </div>
      </template>
    </div>
  </AdminLayout>
</template>
