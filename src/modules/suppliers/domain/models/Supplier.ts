import type { SupplierStatus } from '../enums/SupplierStatus'

export interface Supplier {
  id: string
  supplierName: string
  category: string
  contactPerson: string
  contactNumber: string
  email: string
  address: string
  philgepsRefId: string
  status: SupplierStatus
  totalContracts: number
  totalContractValue: number
  registeredDate: string
}
