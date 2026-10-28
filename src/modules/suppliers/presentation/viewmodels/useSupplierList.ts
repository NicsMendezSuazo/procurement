import { ref, computed, onMounted } from 'vue'
import type { Supplier } from '../../domain/models/Supplier'
import { SupplierStatus } from '../../domain/enums/SupplierStatus'
import { MockSupplierRepository } from '../../infrastructure/repositories/MockSupplierRepository'

const repository = new MockSupplierRepository()

export function useSupplierList() {
  const suppliers = ref<Supplier[]>([])
  const isLoading = ref(false)
  const searchFilter = ref('')
  const categoryFilter = ref<string>('all')

  const filteredSuppliers = computed(() => {
    let result = suppliers.value
    if (categoryFilter.value !== 'all') {
      result = result.filter((s) => s.category === categoryFilter.value)
    }
    const query = searchFilter.value.trim().toLowerCase()
    if (query) {
      result = result.filter(
        (s) =>
          s.supplierName.toLowerCase().includes(query) ||
          s.philgepsRefId.toLowerCase().includes(query) ||
          s.contactPerson.toLowerCase().includes(query),
      )
    }
    return result
  })

  const metrics = computed(() => ({
    active: suppliers.value.filter((s) => s.status === SupplierStatus.Active).length,
    pending: suppliers.value.filter((s) => s.status === SupplierStatus.Pending).length,
    totalContractValue: suppliers.value.reduce((s, sup) => s + sup.totalContractValue, 0),
  }))

  const categories = computed(() => {
    const cats = new Set(suppliers.value.map((s) => s.category))
    return Array.from(cats)
  })

  async function fetchSuppliers() {
    isLoading.value = true
    try {
      suppliers.value = await repository.getAll()
    } finally {
      isLoading.value = false
    }
  }

  function resetFilters() {
    searchFilter.value = ''
    categoryFilter.value = 'all'
  }

  onMounted(fetchSuppliers)

  return {
    suppliers,
    isLoading,
    searchFilter,
    categoryFilter,
    filteredSuppliers,
    metrics,
    categories,
    fetchSuppliers,
    resetFilters,
    SupplierStatus,
  }
}
