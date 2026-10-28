import type { PurchaseOrder } from '../models/PurchaseOrder'
import type { CreatePurchaseOrderDto } from '../../application/dtos/CreatePurchaseOrderDto'

export interface IPurchaseOrderRepository {
  getAll(): Promise<PurchaseOrder[]>
  getById(id: string): Promise<PurchaseOrder | null>
  create(dto: CreatePurchaseOrderDto): Promise<PurchaseOrder>
  approve(id: string): Promise<PurchaseOrder>
}
