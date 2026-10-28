<script setup lang="ts">
import type { POItem } from '../../domain/models/PurchaseOrder'
import { formatCurrency, formatNumber } from '../../../../core/utils/formatters'

defineProps<{ items: POItem[] }>()
</script>

<template>
  <div class="overflow-x-auto">
    <table class="w-full text-sm">
      <thead>
        <tr class="text-left border-b border-slate-200">
          <th class="py-2.5 px-3 font-semibold text-slate-600 text-xs uppercase tracking-wider">Item Description</th>
          <th class="py-2.5 px-3 font-semibold text-slate-600 text-xs uppercase tracking-wider text-right">Qty</th>
          <th class="py-2.5 px-3 font-semibold text-slate-600 text-xs uppercase tracking-wider text-right">Unit Cost</th>
          <th class="py-2.5 px-3 font-semibold text-slate-600 text-xs uppercase tracking-wider text-right">Total Cost</th>
        </tr>
      </thead>
      <tbody class="divide-y divide-slate-100">
        <tr v-for="item in items" :key="item.id" class="hover:bg-slate-50 transition-colors">
          <td class="py-3 px-3 text-slate-800">{{ item.itemDescription }}</td>
          <td class="py-3 px-3 text-right text-slate-600 tabular-nums">{{ formatNumber(item.quantity) }}</td>
          <td class="py-3 px-3 text-right text-slate-600 tabular-nums">{{ formatCurrency(item.unitCost) }}</td>
          <td class="py-3 px-3 text-right text-slate-800 font-semibold tabular-nums">{{ formatCurrency(item.totalCost) }}</td>
        </tr>
      </tbody>
      <tfoot>
        <tr class="border-t-2 border-slate-200 bg-slate-50">
          <td colspan="3" class="py-3 px-3 text-right font-semibold text-slate-700">Grand Total</td>
          <td class="py-3 px-3 text-right font-bold text-slate-900 tabular-nums">
            {{ formatCurrency(items.reduce((sum, i) => sum + i.totalCost, 0)) }}
          </td>
        </tr>
      </tfoot>
    </table>
  </div>
</template>
