import { ref, computed, onMounted } from 'vue'
import type { PurchaseOrder } from '../../domain/models/PurchaseOrder'
import { POStatus, POStatusLabels } from '../../domain/enums/POStatus'
import { GetPurchaseOrdersUseCase } from '../../application/usecases/GetPurchaseOrdersUseCase'
import { ApprovePurchaseOrderUseCase } from '../../application/usecases/ApprovePurchaseOrderUseCase'
import { MockPurchaseOrderRepository } from '../../infrastructure/repositories/MockPurchaseOrderRepository'

const repository = new MockPurchaseOrderRepository()
const getUseCase = new GetPurchaseOrdersUseCase(repository)
const approveUseCase = new ApprovePurchaseOrderUseCase(repository)

export interface POMetrics {
  totalApproved: number
  pendingBAC: number
  totalBudgetAllocated: number
}

export function usePurchaseOrderList() {
  const orders = ref<PurchaseOrder[]>([])
  const isLoading = ref(false)
  const error = ref<string | null>(null)
  const searchFilter = ref('')
  const statusFilter = ref<POStatus | 'all'>('all')

  const filteredOrders = computed(() => {
    let result = orders.value

    if (statusFilter.value !== 'all') {
      result = result.filter((o) => o.status === statusFilter.value)
    }

    const query = searchFilter.value.trim().toLowerCase()
    if (query) {
      result = result.filter(
        (o) =>
          o.poNumber.toLowerCase().includes(query) ||
          o.supplierName.toLowerCase().includes(query) ||
          o.prNumber.toLowerCase().includes(query),
      )
    }

    return result
  })

  const metrics = computed<POMetrics>(() => {
    const approved = orders.value.filter((o) => o.status === POStatus.Approved)
    const pending = orders.value.filter((o) => o.status === POStatus.PendingBAC)
    return {
      totalApproved: approved.length,
      pendingBAC: pending.length,
      totalBudgetAllocated: approved.reduce((sum, o) => sum + o.totalAmount, 0),
    }
  })

  const statusCounts = computed(() => {
    const counts: Record<string, number> = {}
    for (const status of Object.values(POStatus)) {
      counts[status] = orders.value.filter((o) => o.status === status).length
    }
    return counts
  })

  async function fetchOrders() {
    isLoading.value = true
    error.value = null
    try {
      orders.value = await getUseCase.execute()
    } catch (e) {
      error.value = e instanceof Error ? e.message : 'Failed to load purchase orders'
    } finally {
      isLoading.value = false
    }
  }

  async function approveOrder(id: string) {
    try {
      const updated = await approveUseCase.execute(id)
      const index = orders.value.findIndex((o) => o.id === id)
      if (index !== -1) {
        orders.value[index] = updated
      }
    } catch (e) {
      error.value = e instanceof Error ? e.message : 'Failed to approve order'
    }
  }

  function resetFilters() {
    searchFilter.value = ''
    statusFilter.value = 'all'
  }

  onMounted(fetchOrders)

  return {
    orders,
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
    POStatus,
    POStatusLabels,
  }
}
