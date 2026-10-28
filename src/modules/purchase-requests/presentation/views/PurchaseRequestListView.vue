<script setup lang="ts">
import {
  FileText,
  Search,
  Clock,
  CheckCircle2,
  Wallet,
  RotateCcw,
  Loader2,
  AlertTriangle,
} from 'lucide-vue-next'
import AdminLayout from '../../../../shared/layouts/AdminLayout.vue'
import BaseBadge from '../../../../shared/components/BaseBadge.vue'
import BaseButton from '../../../../shared/components/BaseButton.vue'
import { usePurchaseRequestList } from '../viewmodels/usePurchaseRequestList'
import { PRStatus, PRStatusLabels, PRStatusVariants, type PRStatusVariant } from '../../domain/enums/PRStatus'
import { formatCurrency, formatDate } from '../../../../core/utils/formatters'

const {
  isLoading,
  searchFilter,
  statusFilter,
  filteredRequests,
  metrics,
  resetFilters,
} = usePurchaseRequestList()

const statusFilterOptions: { value: PRStatus | 'all'; label: string }[] = [
  { value: 'all', label: 'All Status' },
  ...Object.values(PRStatus).map((s) => ({ value: s, label: PRStatusLabels[s] })),
]

const priorityVariants: Record<string, 'gray' | 'amber' | 'red'> = {
  Low: 'gray',
  Medium: 'gray',
  High: 'amber',
  Urgent: 'red',
}

const kpiCards = [
  { label: 'For Review', value: () => metrics.value.forReview.toString(), sub: 'Awaiting endorsement', icon: Clock, color: 'amber' },
  { label: 'Endorsed', value: () => metrics.value.endorsed.toString(), sub: 'Ready for PO conversion', icon: CheckCircle2, color: 'emerald' },
  { label: 'Total Estimated Value', value: () => formatCurrency(metrics.value.totalEstimated), sub: 'All requests', icon: Wallet, color: 'brand' },
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
          <FileText :size="14" />
          <span>Procurement</span><span>/</span>
          <span class="text-slate-700 font-medium">Purchase Requests</span>
        </div>
        <h1 class="text-2xl font-bold text-slate-900">Purchase Requests</h1>
        <p class="text-sm text-slate-500 mt-1">Departmental procurement requests awaiting BAC endorsement</p>
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
            <input v-model="searchFilter" type="text" placeholder="Search by PR number, department, or item..." class="w-full pl-9 pr-4 py-2.5 text-sm border border-slate-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-brand-400 focus:border-transparent" />
          </div>
          <div class="flex gap-3">
            <select v-model="statusFilter" class="px-3 py-2.5 text-sm border border-slate-300 rounded-lg bg-white focus:outline-none focus:ring-2 focus:ring-brand-400 focus:border-transparent">
              <option v-for="opt in statusFilterOptions" :key="opt.value" :value="opt.value">{{ opt.label }}</option>
            </select>
            <BaseButton variant="secondary" @click="resetFilters"><RotateCcw :size="14" /> Reset</BaseButton>
          </div>
        </div>
      </div>

      <div class="bg-white rounded-xl border border-slate-200 shadow-sm overflow-hidden">
        <div class="overflow-x-auto">
          <table class="w-full text-sm">
            <thead class="bg-slate-50 border-b border-slate-200">
              <tr>
                <th class="text-left py-3 px-4 font-semibold text-slate-600 text-xs uppercase tracking-wider">PR Number</th>
                <th class="text-left py-3 px-4 font-semibold text-slate-600 text-xs uppercase tracking-wider">Department</th>
                <th class="text-left py-3 px-4 font-semibold text-slate-600 text-xs uppercase tracking-wider hidden lg:table-cell">Item Summary</th>
                <th class="text-center py-3 px-4 font-semibold text-slate-600 text-xs uppercase tracking-wider hidden md:table-cell">Items</th>
                <th class="text-right py-3 px-4 font-semibold text-slate-600 text-xs uppercase tracking-wider">Est. Amount</th>
                <th class="text-center py-3 px-4 font-semibold text-slate-600 text-xs uppercase tracking-wider">Priority</th>
                <th class="text-center py-3 px-4 font-semibold text-slate-600 text-xs uppercase tracking-wider">Status</th>
                <th class="text-left py-3 px-4 font-semibold text-slate-600 text-xs uppercase tracking-wider hidden sm:table-cell">Needed By</th>
              </tr>
            </thead>
            <tbody class="divide-y divide-slate-100">
              <tr v-if="isLoading">
                <td colspan="8" class="py-16 text-center">
                  <Loader2 :size="28" class="animate-spin text-brand-500 mx-auto" />
                  <p class="text-sm text-slate-500 mt-3">Loading purchase requests...</p>
                </td>
              </tr>
              <tr v-else-if="filteredRequests.length === 0">
                <td colspan="8" class="py-16 text-center">
                  <FileText :size="32" class="text-slate-300 mx-auto" />
                  <p class="text-sm font-medium text-slate-600 mt-3">No purchase requests found</p>
                </td>
              </tr>
              <tr v-for="req in filteredRequests" :key="req.id" class="hover:bg-slate-50 transition-colors">
                <td class="py-3.5 px-4"><span class="font-semibold text-brand-700">{{ req.prNumber }}</span></td>
                <td class="py-3.5 px-4">
                  <p class="text-slate-700">{{ req.requestingDepartment }}</p>
                  <p class="text-xs text-slate-400">{{ req.requestedBy }}</p>
                </td>
                <td class="py-3.5 px-4 text-slate-500 hidden lg:table-cell max-w-xs truncate">{{ req.itemSummary }}</td>
                <td class="py-3.5 px-4 text-center text-slate-600 hidden md:table-cell tabular-nums">{{ req.itemCount }}</td>
                <td class="py-3.5 px-4 text-right font-semibold text-slate-900 tabular-nums">{{ formatCurrency(req.estimatedAmount) }}</td>
                <td class="py-3.5 px-4 text-center">
                  <BaseBadge :variant="priorityVariants[req.priority]">
                    <AlertTriangle v-if="req.priority === 'Urgent'" :size="12" />
                    {{ req.priority }}
                  </BaseBadge>
                </td>
                <td class="py-3.5 px-4 text-center">
                  <BaseBadge :variant="PRStatusVariants[req.status] as PRStatusVariant">{{ PRStatusLabels[req.status] }}</BaseBadge>
                </td>
                <td class="py-3.5 px-4 text-slate-500 hidden sm:table-cell">{{ formatDate(req.neededBy) }}</td>
              </tr>
            </tbody>
          </table>
        </div>
        <div class="px-4 py-3 border-t border-slate-200 bg-slate-50 flex items-center justify-between text-xs text-slate-500">
          <span>Showing {{ filteredRequests.length }} purchase requests</span>
        </div>
      </div>
    </div>
  </AdminLayout>
</template>
