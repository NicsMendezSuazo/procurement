import { ref, computed, onMounted } from 'vue'
import type { PurchaseRequest } from '../../domain/models/PurchaseRequest'
import { PRStatus, PRStatusLabels } from '../../domain/enums/PRStatus'
import { MockPurchaseRequestRepository } from '../../infrastructure/repositories/MockPurchaseRequestRepository'

const repository = new MockPurchaseRequestRepository()

export function usePurchaseRequestList() {
  const requests = ref<PurchaseRequest[]>([])
  const isLoading = ref(false)
  const searchFilter = ref('')
  const statusFilter = ref<PRStatus | 'all'>('all')

  const filteredRequests = computed(() => {
    let result = requests.value
    if (statusFilter.value !== 'all') {
      result = result.filter((r) => r.status === statusFilter.value)
    }
    const query = searchFilter.value.trim().toLowerCase()
    if (query) {
      result = result.filter(
        (r) =>
          r.prNumber.toLowerCase().includes(query) ||
          r.requestingDepartment.toLowerCase().includes(query) ||
          r.itemSummary.toLowerCase().includes(query),
      )
    }
    return result
  })

  const metrics = computed(() => ({
    forReview: requests.value.filter((r) => r.status === PRStatus.ForReview).length,
    endorsed: requests.value.filter((r) => r.status === PRStatus.Endorsed).length,
    totalEstimated: requests.value.reduce((s, r) => s + r.estimatedAmount, 0),
  }))

  async function fetchRequests() {
    isLoading.value = true
    try {
      requests.value = await repository.getAll()
    } finally {
      isLoading.value = false
    }
  }

  function resetFilters() {
    searchFilter.value = ''
    statusFilter.value = 'all'
  }

  onMounted(fetchRequests)

  return {
    requests,
    isLoading,
    searchFilter,
    statusFilter,
    filteredRequests,
    metrics,
    fetchRequests,
    resetFilters,
    PRStatus,
    PRStatusLabels,
  }
}
