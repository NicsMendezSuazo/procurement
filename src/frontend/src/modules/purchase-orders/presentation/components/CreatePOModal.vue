<script setup lang="ts">
import { Plus, Trash2, Loader2 } from 'lucide-vue-next'
import BaseModal from '../../../../shared/components/BaseModal.vue'
import BaseButton from '../../../../shared/components/BaseButton.vue'
import { formatCurrency } from '../../../../core/utils/formatters'
import { ModeOfProcurementLabels } from '../../domain/enums/POStatus'
import { useCreatePurchaseOrder } from '../viewmodels/useCreatePurchaseOrder'

const props = defineProps<{ onCreated: () => void }>()

const {
  isVisible,
  isSubmitting,
  error,
  form,
  items,
  computedTotal,
  isFormValid,
  addItem,
  removeItem,
  openModal,
  closeModal,
  submit,
} = useCreatePurchaseOrder(props.onCreated)

defineExpose({ openModal })
</script>

<template>
  <BaseModal v-model="isVisible" title="Create Purchase Order" max-width="max-w-3xl">
    <div class="space-y-5">
      <div v-if="error" class="bg-red-50 border border-red-200 text-red-700 text-sm rounded-lg px-4 py-3">
        {{ error }}
      </div>

      <!-- Header Fields -->
      <div class="grid grid-cols-1 md:grid-cols-2 gap-4">
        <div>
          <label class="block text-sm font-medium text-slate-700 mb-1">PO Number</label>
          <input
            v-model="form.poNumber"
            type="text"
            placeholder="PO-2025-DRMC-0149"
            class="w-full px-3 py-2 text-sm border border-slate-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-brand-400 focus:border-transparent"
          />
        </div>
        <div>
          <label class="block text-sm font-medium text-slate-700 mb-1">PR Number</label>
          <input
            v-model="form.prNumber"
            type="text"
            placeholder="PR-2025-0420"
            class="w-full px-3 py-2 text-sm border border-slate-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-brand-400 focus:border-transparent"
          />
        </div>
        <div>
          <label class="block text-sm font-medium text-slate-700 mb-1">Supplier Name</label>
          <input
            v-model="form.supplierName"
            type="text"
            placeholder="Zuellig Pharma Corporation"
            class="w-full px-3 py-2 text-sm border border-slate-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-brand-400 focus:border-transparent"
          />
        </div>
        <div>
          <label class="block text-sm font-medium text-slate-700 mb-1">Mode of Procurement</label>
          <select
            v-model="form.modeOfProcurement"
            class="w-full px-3 py-2 text-sm border border-slate-300 rounded-lg bg-white focus:outline-none focus:ring-2 focus:ring-brand-400 focus:border-transparent"
          >
            <option v-for="(label, key) in ModeOfProcurementLabels" :key="key" :value="key">
              {{ label }}
            </option>
          </select>
        </div>
      </div>

      <!-- Items -->
      <div>
        <div class="flex items-center justify-between mb-2">
          <label class="text-sm font-semibold text-slate-700">Line Items</label>
          <BaseButton variant="ghost" size="sm" @click="addItem">
            <Plus :size="14" />
            Add Item
          </BaseButton>
        </div>

        <div class="space-y-2">
          <div
            v-for="(item, index) in items"
            :key="index"
            class="grid grid-cols-12 gap-2 items-start bg-slate-50 border border-slate-200 rounded-lg p-3"
          >
            <div class="col-span-6">
              <input
                v-model="item.itemDescription"
                type="text"
                placeholder="Item description"
                class="w-full px-3 py-2 text-sm border border-slate-300 rounded-lg bg-white focus:outline-none focus:ring-2 focus:ring-brand-400 focus:border-transparent"
              />
            </div>
            <div class="col-span-2">
              <input
                v-model="item.quantity"
                type="number"
                placeholder="Qty"
                class="w-full px-3 py-2 text-sm border border-slate-300 rounded-lg bg-white text-right focus:outline-none focus:ring-2 focus:ring-brand-400 focus:border-transparent"
              />
            </div>
            <div class="col-span-3">
              <input
                v-model="item.unitCost"
                type="number"
                placeholder="Unit Cost (₱)"
                class="w-full px-3 py-2 text-sm border border-slate-300 rounded-lg bg-white text-right focus:outline-none focus:ring-2 focus:ring-brand-400 focus:border-transparent"
              />
            </div>
            <div class="col-span-1 flex items-center justify-center pt-1">
              <button
                v-if="items.length > 1"
                class="text-red-400 hover:text-red-600 hover:bg-red-50 rounded p-1.5 transition-colors"
                @click="removeItem(index)"
              >
                <Trash2 :size="16" />
              </button>
            </div>
          </div>
        </div>
      </div>

      <!-- Total -->
      <div class="flex justify-end items-center gap-2 pt-2 border-t border-slate-200">
        <span class="text-sm font-medium text-slate-600">Estimated Total:</span>
        <span class="text-lg font-bold text-slate-900 tabular-nums">{{ formatCurrency(computedTotal) }}</span>
      </div>
    </div>

    <template #footer>
      <div class="flex items-center justify-end gap-3">
        <BaseButton variant="secondary" @click="closeModal">Cancel</BaseButton>
        <BaseButton variant="success" :disabled="!isFormValid || isSubmitting" @click="submit">
          <Loader2 v-if="isSubmitting" :size="16" class="animate-spin" />
          Create Order
        </BaseButton>
      </div>
    </template>
  </BaseModal>
</template>
