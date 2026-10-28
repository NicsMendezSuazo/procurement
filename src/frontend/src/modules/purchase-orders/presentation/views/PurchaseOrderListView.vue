<script setup lang="ts">
import { ref } from 'vue'
import {
  CheckCircle2,
  Clock,
  Wallet,
  Search,
  Plus,
  Eye,
  CheckSquare,
  X,
  Package,
  TrendingUp,
  RotateCcw,
  Loader2,
} from 'lucide-vue-next'
import AdminLayout from '../../../../shared/layouts/AdminLayout.vue'
import BaseButton from '../../../../shared/components/BaseButton.vue'
import BaseModal from '../../../../shared/components/BaseModal.vue'
import POStatusBadge from '../components/POStatusBadge.vue'
import POItemsTable from '../components/POItemsTable.vue'
import CreatePOModal from '../components/CreatePOModal.vue'
import { usePurchaseOrderList } from '../viewmodels/usePurchaseOrderList'
import { GetPurchaseOrderDetailUseCase } from '../../application/usecases/GetPurchaseOrderDetailUseCase'
import { MockPurchaseOrderRepository } from '../../infrastructure/repositories/MockPurchaseOrderRepository'
import { POStatus, POStatusLabels, ModeOfProcurementLabels } from '../../domain/enums/POStatus'
import { formatCurrency, formatDate } from '../../../../core/utils/formatters'
import type { PurchaseOrder } from '../../domain/models/PurchaseOrder'

const {
  isLoading,
  error,
  searchFilter,
  statusFilter,
  filteredOrders,
  metrics,
  statusCounts,
  fetchOrders,
  approveOrder,
  resetFilters,
} = usePurchaseOrderList()

const createModalRef = ref<InstanceType<typeof CreatePOModal> | null>(null)
const detailModalOpen = ref(false)
const detailOrder = ref<PurchaseOrder | null>(null)
const detailLoading = ref(false)

const repository = new MockPurchaseOrderRepository()
const detailUseCase = new GetPurchaseOrderDetailUseCase(repository)

const statusFilterOptions: { value: POStatus | 'all'; label: string }[] = [
  { value: 'all', label: 'All Status' },
  { value: POStatus.Draft, label: POStatusLabels[POStatus.Draft] },
  { value: POStatus.PendingBAC, label: POStatusLabels[POStatus.PendingBAC] },
  { value: POStatus.Approved, label: POStatusLabels[POStatus.Approved] },
  { value: POStatus.Delivered, label: POStatusLabels[POStatus.Delivered] },
  { value: POStatus.Cancelled, label: POStatusLabels[POStatus.Cancelled] },
]

const kpiCards = [
  {
    label: 'Total Approved',
    value: () => metrics.value.totalApproved.toString(),
    sub: 'Purchase Orders',
    icon: CheckCircle2,
    color: 'emerald',
  },
  {
    label: 'Pending BAC',
    value: () => metrics.value.pendingBAC.toString(),
    sub: 'Awaiting Review',
    icon: Clock,
    color: 'amber',
  },
  {
    label: 'Total Budget Allocated',
    value: () => formatCurrency(metrics.value.totalBudgetAllocated),
    sub: 'Approved PO Value',
    icon: Wallet,
    color: 'brand',
  },
]

const colorClasses: Record<string, { bg: string; text: string; ring: string }> = {
  emerald: { bg: 'bg-emerald-50', text: 'text-emerald-600', ring: 'ring-emerald-100' },
  amber: { bg: 'bg-amber-50', text: 'text-amber-600', ring: 'ring-amber-100' },
  brand: { bg: 'bg-brand-50', text: 'text-brand-600', ring: 'ring-brand-100' },
}

async function viewDetail(id: string) {
  detailModalOpen.value = true
  detailLoading.value = true
  detailOrder.value = null
  try {
    detailOrder.value = await detailUseCase.execute(id)
  } finally {
    detailLoading.value = false
  }
}

async function handleApprove(id: string) {
  await approveOrder(id)
  if (detailOrder.value?.id === id) {
    detailOrder.value = await detailUseCase.execute(id)
  }
}

function handleCreated() {
  fetchOrders()
}
</script>

<template>
  <AdminLayout>
    <div class="max-w-7xl mx-auto">
      <!-- Page Header -->
      <div class="mb-6">
        <div class="flex items-center gap-2 text-sm text-slate-500 mb-1">
          <Package :size="14" />
          <span>Procurement</span>
          <span>/</span>
          <span class="text-slate-700 font-medium">Purchase Orders</span>
        </div>
        <div class="flex flex-col sm:flex-row sm:items-center sm:justify-between gap-3">
          <div>
            <h1 class="text-2xl font-bold text-slate-900">Purchase Order Management</h1>
            <p class="text-sm text-slate-500 mt-1">
              Davao Regional Medical Center — BAC Secretariat Portal
            </p>
          </div>
          <BaseButton variant="primary" @click="createModalRef?.openModal()">
            <Plus :size="16" />
            New Purchase Order
          </BaseButton>
        </div>
      </div>

      <!-- KPI Cards -->
      <div class="grid grid-cols-1 sm:grid-cols-3 gap-4 mb-6">
        <div
          v-for="card in kpiCards"
          :key="card.label"
          class="bg-white rounded-xl border border-slate-200 p-5 shadow-sm hover:shadow-md transition-shadow"
        >
          <div class="flex items-start justify-between">
            <div>
              <p class="text-sm font-medium text-slate-500">{{ card.label }}</p>
              <p class="text-2xl font-bold text-slate-900 mt-1 tabular-nums">{{ card.value() }}</p>
              <p class="text-xs text-slate-400 mt-1">{{ card.sub }}</p>
            </div>
            <div
              :class="[
                'flex items-center justify-center w-11 h-11 rounded-lg ring-8',
                colorClasses[card.color].bg,
                colorClasses[card.color].text,
                colorClasses[card.color].ring,
              ]"
            >
              <component :is="card.icon" :size="22" />
            </div>
          </div>
        </div>
      </div>

      <!-- Filter Bar -->
      <div class="bg-white rounded-xl border border-slate-200 p-4 mb-4 shadow-sm">
        <div class="flex flex-col md:flex-row gap-3">
          <div class="relative flex-1">
            <Search :size="16" class="absolute left-3 top-1/2 -translate-y-1/2 text-slate-400" />
            <input
              v-model="searchFilter"
              type="text"
              placeholder="Search by PO number, supplier, or PR number..."
              class="w-full pl-9 pr-4 py-2.5 text-sm border border-slate-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-brand-400 focus:border-transparent"
            />
          </div>
          <div class="flex gap-3">
            <select
              v-model="statusFilter"
              class="px-3 py-2.5 text-sm border border-slate-300 rounded-lg bg-white focus:outline-none focus:ring-2 focus:ring-brand-400 focus:border-transparent"
            >
              <option v-for="opt in statusFilterOptions" :key="opt.value" :value="opt.value">
                {{ opt.label }}
                <template v-if="opt.value !== 'all' && statusCounts[opt.value]">
                  ({{ statusCounts[opt.value] }})
                </template>
              </option>
            </select>
            <BaseButton variant="secondary" @click="resetFilters">
              <RotateCcw :size="14" />
              Reset
            </BaseButton>
          </div>
        </div>
      </div>

      <!-- Error Banner -->
      <div v-if="error" class="bg-red-50 border border-red-200 text-red-700 text-sm rounded-lg px-4 py-3 mb-4">
        {{ error }}
      </div>

      <!-- Data Table -->
      <div class="bg-white rounded-xl border border-slate-200 shadow-sm overflow-hidden">
        <div class="overflow-x-auto">
          <table class="w-full text-sm">
            <thead class="bg-slate-50 border-b border-slate-200">
              <tr>
                <th class="text-left py-3 px-4 font-semibold text-slate-600 text-xs uppercase tracking-wider">PO Number</th>
                <th class="text-left py-3 px-4 font-semibold text-slate-600 text-xs uppercase tracking-wider">Supplier</th>
                <th class="text-left py-3 px-4 font-semibold text-slate-600 text-xs uppercase tracking-wider hidden lg:table-cell">PR Ref</th>
                <th class="text-left py-3 px-4 font-semibold text-slate-600 text-xs uppercase tracking-wider hidden md:table-cell">Mode</th>
                <th class="text-right py-3 px-4 font-semibold text-slate-600 text-xs uppercase tracking-wider">Total Amount</th>
                <th class="text-center py-3 px-4 font-semibold text-slate-600 text-xs uppercase tracking-wider">Status</th>
                <th class="text-left py-3 px-4 font-semibold text-slate-600 text-xs uppercase tracking-wider hidden sm:table-cell">Date</th>
                <th class="text-right py-3 px-4 font-semibold text-slate-600 text-xs uppercase tracking-wider">Actions</th>
              </tr>
            </thead>
            <tbody class="divide-y divide-slate-100">
              <!-- Loading -->
              <tr v-if="isLoading">
                <td colspan="8" class="py-16 text-center">
                  <Loader2 :size="28" class="animate-spin text-brand-500 mx-auto" />
                  <p class="text-sm text-slate-500 mt-3">Loading purchase orders...</p>
                </td>
              </tr>
              <!-- Empty -->
              <tr v-else-if="filteredOrders.length === 0">
                <td colspan="8" class="py-16 text-center">
                  <Package :size="32" class="text-slate-300 mx-auto" />
                  <p class="text-sm font-medium text-slate-600 mt-3">No purchase orders found</p>
                  <p class="text-xs text-slate-400 mt-1">Try adjusting your search or filters</p>
                </td>
              </tr>
              <!-- Rows -->
              <tr
                v-for="order in filteredOrders"
                :key="order.id"
                class="hover:bg-slate-50 transition-colors"
              >
                <td class="py-3.5 px-4">
                  <span class="font-semibold text-brand-700">{{ order.poNumber }}</span>
                </td>
                <td class="py-3.5 px-4 text-slate-700">{{ order.supplierName }}</td>
                <td class="py-3.5 px-4 text-slate-500 hidden lg:table-cell">{{ order.prNumber }}</td>
                <td class="py-3.5 px-4 text-slate-500 hidden md:table-cell">
                  {{ ModeOfProcurementLabels[order.modeOfProcurement] }}
                </td>
                <td class="py-3.5 px-4 text-right font-semibold text-slate-900 tabular-nums">
                  {{ formatCurrency(order.totalAmount) }}
                </td>
                <td class="py-3.5 px-4 text-center">
                  <POStatusBadge :status="order.status" />
                </td>
                <td class="py-3.5 px-4 text-slate-500 hidden sm:table-cell">{{ formatDate(order.createdAt) }}</td>
                <td class="py-3.5 px-4">
                  <div class="flex items-center justify-end gap-1">
                    <button
                      class="text-slate-400 hover:text-brand-600 hover:bg-brand-50 rounded-lg p-1.5 transition-colors"
                      title="View details"
                      @click="viewDetail(order.id)"
                    >
                      <Eye :size="16" />
                    </button>
                    <button
                      v-if="order.status === POStatus.PendingBAC || order.status === POStatus.Draft"
                      class="text-slate-400 hover:text-emerald-600 hover:bg-emerald-50 rounded-lg p-1.5 transition-colors"
                      title="Approve"
                      @click="handleApprove(order.id)"
                    >
                      <CheckSquare :size="16" />
                    </button>
                  </div>
                </td>
              </tr>
            </tbody>
          </table>
        </div>
        <!-- Footer -->
        <div class="px-4 py-3 border-t border-slate-200 bg-slate-50 flex items-center justify-between text-xs text-slate-500">
          <span>Showing {{ filteredOrders.length }} of {{ isLoading ? '...' : statusCounts[POStatus.Draft] + statusCounts[POStatus.PendingBAC] + statusCounts[POStatus.Approved] + statusCounts[POStatus.Delivered] + statusCounts[POStatus.Cancelled] }} purchase orders</span>
          <span class="flex items-center gap-1">
            <TrendingUp :size="12" />
            PhilGEPS Registered
          </span>
        </div>
      </div>
    </div>

    <!-- Detail Modal -->
    <BaseModal v-model="detailModalOpen" title="Purchase Order Details" max-width="max-w-3xl">
      <div v-if="detailLoading" class="py-16 text-center">
        <Loader2 :size="28" class="animate-spin text-brand-500 mx-auto" />
        <p class="text-sm text-slate-500 mt-3">Loading details...</p>
      </div>
      <div v-else-if="detailOrder" class="space-y-5">
        <!-- Header Info -->
        <div class="flex items-start justify-between">
          <div>
            <h3 class="text-lg font-bold text-slate-900">{{ detailOrder.poNumber }}</h3>
            <p class="text-sm text-slate-500">{{ detailOrder.supplierName }}</p>
          </div>
          <POStatusBadge :status="detailOrder.status" />
        </div>

        <!-- Meta Grid -->
        <div class="grid grid-cols-2 md:grid-cols-4 gap-4 bg-slate-50 rounded-lg p-4">
          <div>
            <p class="text-xs font-medium text-slate-400 uppercase tracking-wider">PR Reference</p>
            <p class="text-sm font-semibold text-slate-700 mt-0.5">{{ detailOrder.prNumber }}</p>
          </div>
          <div>
            <p class="text-xs font-medium text-slate-400 uppercase tracking-wider">Mode</p>
            <p class="text-sm font-semibold text-slate-700 mt-0.5">{{ ModeOfProcurementLabels[detailOrder.modeOfProcurement] }}</p>
          </div>
          <div>
            <p class="text-xs font-medium text-slate-400 uppercase tracking-wider">Date Created</p>
            <p class="text-sm font-semibold text-slate-700 mt-0.5">{{ formatDate(detailOrder.createdAt) }}</p>
          </div>
          <div>
            <p class="text-xs font-medium text-slate-400 uppercase tracking-wider">Total Amount</p>
            <p class="text-sm font-bold text-brand-700 mt-0.5">{{ formatCurrency(detailOrder.totalAmount) }}</p>
          </div>
        </div>

        <!-- Items -->
        <div>
          <p class="text-sm font-semibold text-slate-700 mb-2">Line Items ({{ detailOrder.items.length }})</p>
          <div class="border border-slate-200 rounded-lg overflow-hidden">
            <POItemsTable :items="detailOrder.items" />
          </div>
        </div>
      </div>
      <div v-else class="py-12 text-center">
        <X :size="28" class="text-slate-300 mx-auto" />
        <p class="text-sm text-slate-500 mt-3">Unable to load purchase order</p>
      </div>

      <template #footer>
        <div class="flex items-center justify-between gap-3">
          <BaseButton variant="secondary" @click="detailModalOpen = false">Close</BaseButton>
          <BaseButton
            v-if="detailOrder && (detailOrder.status === POStatus.PendingBAC || detailOrder.status === POStatus.Draft)"
            variant="success"
            @click="handleApprove(detailOrder.id)"
          >
            <CheckSquare :size="16" />
            Approve Order
          </BaseButton>
        </div>
      </template>
    </BaseModal>

    <!-- Create Modal -->
    <CreatePOModal ref="createModalRef" :on-created="handleCreated" />
  </AdminLayout>
</template>
