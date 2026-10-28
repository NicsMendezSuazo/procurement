import type { IPurchaseOrderRepository } from '../../domain/ports/IPurchaseOrderRepository'
import type { PurchaseOrder } from '../../domain/models/PurchaseOrder'

export class GetPurchaseOrderDetailUseCase {
  constructor(private readonly repo: IPurchaseOrderRepository) {}

  async execute(id: string): Promise<PurchaseOrder | null> {
    return this.repo.getById(id)
  }
}
