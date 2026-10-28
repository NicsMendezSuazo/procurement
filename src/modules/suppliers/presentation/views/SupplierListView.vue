<script setup lang="ts">
import {
  Users,
  Search,
  CheckCircle2,
  Clock,
  Wallet,
  RotateCcw,
  Loader2,
  Building2,
  Phone,
  Mail,
  Award,
} from 'lucide-vue-next'
import AdminLayout from '../../../../shared/layouts/AdminLayout.vue'
import BaseBadge from '../../../../shared/components/BaseBadge.vue'
import BaseButton from '../../../../shared/components/BaseButton.vue'
import { useSupplierList } from '../viewmodels/useSupplierList'
import { SupplierStatusLabels, SupplierStatusVariants, type SupplierStatusVariant } from '../../domain/enums/SupplierStatus'
import { formatCurrency, formatDate } from '../../../../core/utils/formatters'

const {
  isLoading,
  searchFilter,
  categoryFilter,
  filteredSuppliers,
  metrics,
  categories,
  resetFilters,
} = useSupplierList()

const kpiCards = [
  { label: 'Active Suppliers', value: () => metrics.value.active.toString(), sub: 'In good standing', icon: CheckCircle2, color: 'emerald' },
  { label: 'Pending Verification', value: () => metrics.value.pending.toString(), sub: 'Awaiting documents', icon: Clock, color: 'amber' },
  { label: 'Total Contract Value', value: () => formatCurrency(metrics.value.totalContractValue), sub: 'All suppliers', icon: Wallet, color: 'brand' },
]

const colorClasses: Record<string, { bg: string; text: string; ring: string }> = {
  emerald: { bg: 'bg-emerald-50', text: 'text-emerald-600', ring: 'ring-emerald-100' },
  amber: { bg: 'bg-amber-50', text: 'text-amber-600', ring: 'ring-amber-100' },
  brand: { bg: 'bg-brand-50', text: 'text-brand-600', ring: 'ring-brand-100' },
}
</script>

<template>
  <AdminLayout>
    <div class="max-w-7xl mx-auto">
      <div class="mb-6">
        <div class="flex items-center gap-2 text-sm text-slate-500 mb-1">
          <Users :size="14" />
          <span>Procurement</span><span>/</span>
          <span class="text-slate-700 font-medium">Suppliers</span>
        </div>
        <h1 class="text-2xl font-bold text-slate-900">Supplier Directory</h1>
        <p class="text-sm text-slate-500 mt-1">PhilGEPS-registered vendors for DRMC procurement</p>
      </div>

      <div class="grid grid-cols-1 sm:grid-cols-3 gap-4 mb-6">
        <div v-for="card in kpiCards" :key="card.label" class="bg-white rounded-xl border border-slate-200 p-5 shadow-sm hover:shadow-md transition-shadow">
          <div class="flex items-start justify-between">
            <div>
              <p class="text-sm font-medium text-slate-500">{{ card.label }}</p>
              <p class="text-2xl font-bold text-slate-900 mt-1 tabular-nums">{{ card.value() }}</p>
              <p class="text-xs text-slate-400 mt-1">{{ card.sub }}</p>
            </div>
            <div :class="['flex items-center justify-center w-11 h-11 rounded-lg ring-8', colorClasses[card.color].bg, colorClasses[card.color].text, colorClasses[card.color].ring]">
              <component :is="card.icon" :size="22" />
            </div>
          </div>
        </div>
      </div>

      <div class="bg-white rounded-xl border border-slate-200 p-4 mb-4 shadow-sm">
        <div class="flex flex-col md:flex-row gap-3">
          <div class="relative flex-1">
            <Search :size="16" class="absolute left-3 top-1/2 -translate-y-1/2 text-slate-400" />
            <input v-model="searchFilter" type="text" placeholder="Search by supplier name, PhilGEPS ID, or contact..." class="w-full pl-9 pr-4 py-2.5 text-sm border border-slate-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-brand-400 focus:border-transparent" />
          </div>
          <div class="flex gap-3">
            <select v-model="categoryFilter" class="px-3 py-2.5 text-sm border border-slate-300 rounded-lg bg-white focus:outline-none focus:ring-2 focus:ring-brand-400 focus:border-transparent">
              <option value="all">All Categories</option>
              <option v-for="cat in categories" :key="cat" :value="cat">{{ cat }}</option>
            </select>
            <BaseButton variant="secondary" @click="resetFilters"><RotateCcw :size="14" /> Reset</BaseButton>
          </div>
        </div>
      </div>

      <div v-if="isLoading" class="flex items-center justify-center py-20">
        <Loader2 :size="32" class="animate-spin text-brand-500" />
      </div>
      <div v-else-if="filteredSuppliers.length === 0" class="bg-white rounded-xl border border-slate-200 py-16 text-center shadow-sm">
        <Users :size="32" class="text-slate-300 mx-auto" />
        <p class="text-sm font-medium text-slate-600 mt-3">No suppliers found</p>
      </div>
      <div v-else class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
        <div v-for="supplier in filteredSuppliers" :key="supplier.id" class="bg-white rounded-xl border border-slate-200 p-5 shadow-sm hover:shadow-md transition-shadow">
          <div class="flex items-start justify-between mb-3">
            <div class="flex items-center gap-3">
              <div class="flex items-center justify-center w-10 h-10 rounded-lg bg-slate-100 text-slate-500">
                <Building2 :size="20" />
              </div>
              <div>
                <p class="font-semibold text-slate-800 text-sm leading-tight">{{ supplier.supplierName }}</p>
                <p class="text-xs text-slate-400">{{ supplier.category }}</p>
              </div>
            </div>
            <BaseBadge :variant="SupplierStatusVariants[supplier.status] as SupplierStatusVariant">{{ SupplierStatusLabels[supplier.status] }}</BaseBadge>
          </div>

          <div class="space-y-2 text-sm border-t border-slate-100 pt-3">
            <div class="flex items-center gap-2 text-slate-600">
              <Award :size="14" class="text-slate-400" />
              <span class="text-xs">{{ supplier.philgepsRefId }}</span>
            </div>
            <div class="flex items-center gap-2 text-slate-600">
              <Phone :size="14" class="text-slate-400" />
              <span class="text-xs">{{ supplier.contactNumber }}</span>
            </div>
            <div class="flex items-center gap-2 text-slate-600">
              <Mail :size="14" class="text-slate-400" />
              <span class="text-xs truncate">{{ supplier.email }}</span>
            </div>
          </div>

          <div class="grid grid-cols-3 gap-2 mt-4 pt-3 border-t border-slate-100">
            <div class="text-center">
              <p class="text-xs text-slate-400">Contracts</p>
              <p class="text-sm font-bold text-slate-700 tabular-nums">{{ supplier.totalContracts }}</p>
            </div>
            <div class="text-center">
              <p class="text-xs text-slate-400">Value</p>
              <p class="text-sm font-bold text-slate-700 tabular-nums">{{ formatCurrency(supplier.totalContractValue).replace('.00', '') }}</p>
            </div>
            <div class="text-center">
              <p class="text-xs text-slate-400">Since</p>
              <p class="text-sm font-bold text-slate-700">{{ formatDate(supplier.registeredDate) }}</p>
            </div>
          </div>
        </div>
      </div>
    </div>
  </AdminLayout>
</template>
