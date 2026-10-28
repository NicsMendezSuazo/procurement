import type { IPurchaseOrderRepository } from '../../domain/ports/IPurchaseOrderRepository'
import type { PurchaseOrder } from '../../domain/models/PurchaseOrder'
import type { CreatePurchaseOrderDto } from '../dtos/CreatePurchaseOrderDto'

export class CreatePurchaseOrderUseCase {
  constructor(private readonly repo: IPurchaseOrderRepository) {}

  async execute(dto: CreatePurchaseOrderDto): Promise<PurchaseOrder> {
    return this.repo.create(dto)
  }
}
