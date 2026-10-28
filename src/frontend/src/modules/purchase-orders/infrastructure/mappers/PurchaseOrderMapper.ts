import type { PurchaseOrder, POItem } from '../../domain/models/PurchaseOrder'
import { POStatus, ModeOfProcurement } from '../../domain/enums/POStatus'
import type { CreatePurchaseOrderDto } from '../../application/dtos/CreatePurchaseOrderDto'

export interface RawPOItem {
  id: string
  itemDescription: string
  quantity: number
  unitCost: number
  totalCost: number
}

export interface RawPurchaseOrder {
  id: string
  poNumber: string
  supplierName: string
  prNumber: string
  modeOfProcurement: ModeOfProcurement
  totalAmount: number
  status: POStatus
  items: RawPOItem[]
  createdAt: string
}

export class PurchaseOrderMapper {
  static toDomain(raw: RawPurchaseOrder): PurchaseOrder {
    return {
      id: raw.id,
      poNumber: raw.poNumber,
      supplierName: raw.supplierName,
      prNumber: raw.prNumber,
      modeOfProcurement: raw.modeOfProcurement,
      totalAmount: raw.totalAmount,
      status: raw.status,
      items: raw.items.map((item) => this.toDomainItem(item)),
      createdAt: raw.createdAt,
    }
  }

  static toDomainItem(raw: RawPOItem): POItem {
    return {
      id: raw.id,
      itemDescription: raw.itemDescription,
      quantity: raw.quantity,
      unitCost: raw.unitCost,
      totalCost: raw.totalCost,
    }
  }

  static fromDto(dto: CreatePurchaseOrderDto, id: string): RawPurchaseOrder {
    const items: RawPOItem[] = dto.items.map((item, idx) => ({
      id: `${id}-item-${idx + 1}`,
      itemDescription: item.itemDescription,
      quantity: item.quantity,
      unitCost: item.unitCost,
      totalCost: item.quantity * item.unitCost,
    }))

    const totalAmount = items.reduce((sum, item) => sum + item.totalCost, 0)

    return {
      id,
      poNumber: dto.poNumber,
      supplierName: dto.supplierName,
      prNumber: dto.prNumber,
      modeOfProcurement: dto.modeOfProcurement,
      totalAmount,
      status: POStatus.Draft,
      items,
      createdAt: new Date().toISOString(),
    }
  }

  static toRaw(po: PurchaseOrder): RawPurchaseOrder {
    return { ...po, items: po.items.map((i) => ({ ...i })) }
  }
}
