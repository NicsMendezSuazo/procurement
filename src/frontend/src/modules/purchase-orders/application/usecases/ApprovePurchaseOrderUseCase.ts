import type { IPurchaseOrderRepository } from '../../domain/ports/IPurchaseOrderRepository'
import type { PurchaseOrder } from '../../domain/models/PurchaseOrder'

export class ApprovePurchaseOrderUseCase {
  constructor(private readonly repo: IPurchaseOrderRepository) {}

  async execute(id: string): Promise<PurchaseOrder> {
    return this.repo.approve(id)
  }
}
