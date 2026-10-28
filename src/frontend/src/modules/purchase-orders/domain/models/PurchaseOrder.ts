import { POStatus, ModeOfProcurement } from '../enums/POStatus'

export interface POItem {
  id: string
  itemDescription: string
  quantity: number
  unitCost: number
  totalCost: number
}

export interface PurchaseOrder {
  id: string
  poNumber: string
  supplierName: string
  prNumber: string
  modeOfProcurement: ModeOfProcurement
  totalAmount: number
  status: POStatus
  items: POItem[]
  createdAt: string
}
