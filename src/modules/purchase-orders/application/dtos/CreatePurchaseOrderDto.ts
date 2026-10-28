import type { ModeOfProcurement } from '../../domain/enums/POStatus'

export interface CreatePOItemDto {
  itemDescription: string
  quantity: number
  unitCost: number
}

export interface CreatePurchaseOrderDto {
  poNumber: string
  supplierName: string
  prNumber: string
  modeOfProcurement: ModeOfProcurement
  items: CreatePOItemDto[]
}
