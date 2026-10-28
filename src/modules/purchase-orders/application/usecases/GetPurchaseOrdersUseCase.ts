import type { IPurchaseOrderRepository } from '../../domain/ports/IPurchaseOrderRepository'
import type { PurchaseOrder } from '../../domain/models/PurchaseOrder'

export class GetPurchaseOrdersUseCase {
  constructor(private readonly repo: IPurchaseOrderRepository) {}

  async execute(): Promise<PurchaseOrder[]> {
    return this.repo.getAll()
  }
}
