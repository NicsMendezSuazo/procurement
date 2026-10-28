import { ref, reactive, computed } from 'vue'
import { CreatePurchaseOrderUseCase } from '../../application/usecases/CreatePurchaseOrderUseCase'
import { MockPurchaseOrderRepository } from '../../infrastructure/repositories/MockPurchaseOrderRepository'
import type { CreatePOItemDto, CreatePurchaseOrderDto } from '../../application/dtos/CreatePurchaseOrderDto'
import type { ModeOfProcurement } from '../../domain/enums/POStatus'

const repository = new MockPurchaseOrderRepository()
const createUseCase = new CreatePurchaseOrderUseCase(repository)

interface FormItem {
  itemDescription: string
  quantity: string
  unitCost: string
}

export function useCreatePurchaseOrder(onCreated?: () => void) {
  const isVisible = ref(false)
  const isSubmitting = ref(false)
  const error = ref<string | null>(null)

  const form = reactive({
    poNumber: '',
    supplierName: '',
    prNumber: '',
    modeOfProcurement: 'Public Bidding' as ModeOfProcurement,
  })

  const items = ref<FormItem[]>([{ itemDescription: '', quantity: '', unitCost: '' }])

  const computedTotal = computed(() => {
    return items.value.reduce((sum, item) => {
      const qty = parseFloat(item.quantity) || 0
      const cost = parseFloat(item.unitCost) || 0
      return sum + qty * cost
    }, 0)
  })

  const isFormValid = computed(() => {
    if (!form.poNumber.trim() || !form.supplierName.trim() || !form.prNumber.trim()) {
      return false
    }
    return items.value.every(
      (item) =>
        item.itemDescription.trim() &&
        parseFloat(item.quantity) > 0 &&
        parseFloat(item.unitCost) >= 0,
    )
  })

  function addItem() {
    items.value.push({ itemDescription: '', quantity: '', unitCost: '' })
  }

  function removeItem(index: number) {
    if (items.value.length > 1) {
      items.value.splice(index, 1)
    }
  }

  function resetForm() {
    form.poNumber = ''
    form.supplierName = ''
    form.prNumber = ''
    form.modeOfProcurement = 'Public Bidding'
    items.value = [{ itemDescription: '', quantity: '', unitCost: '' }]
    error.value = null
  }

  function openModal() {
    resetForm()
    isVisible.value = true
  }

  function closeModal() {
    isVisible.value = false
    resetForm()
  }

  async function submit() {
    if (!isFormValid.value) {
      error.value = 'Please complete all required fields.'
      return
    }
    isSubmitting.value = true
    error.value = null
    try {
      const dtoItems: CreatePOItemDto[] = items.value.map((item) => ({
        itemDescription: item.itemDescription.trim(),
        quantity: parseFloat(item.quantity),
        unitCost: parseFloat(item.unitCost),
      }))
      const dto: CreatePurchaseOrderDto = {
        poNumber: form.poNumber.trim(),
        supplierName: form.supplierName.trim(),
        prNumber: form.prNumber.trim(),
        modeOfProcurement: form.modeOfProcurement,
        items: dtoItems,
      }
      await createUseCase.execute(dto)
      closeModal()
      onCreated?.()
    } catch (e) {
      error.value = e instanceof Error ? e.message : 'Failed to create purchase order'
    } finally {
      isSubmitting.value = false
    }
  }

  return {
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
    resetForm,
  }
}
