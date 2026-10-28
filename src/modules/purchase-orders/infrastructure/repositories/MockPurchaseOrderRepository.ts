import type { IPurchaseOrderRepository } from '../../domain/ports/IPurchaseOrderRepository'
import type { PurchaseOrder } from '../../domain/models/PurchaseOrder'
import { POStatus } from '../../domain/enums/POStatus'
import type { CreatePurchaseOrderDto } from '../../application/dtos/CreatePurchaseOrderDto'
import { PurchaseOrderMapper, type RawPurchaseOrder } from '../mappers/PurchaseOrderMapper'

const MOCK_DELAY = 400

const seedData: RawPurchaseOrder[] = [
  {
    id: 'po-001',
    poNumber: 'PO-2025-DRMC-0142',
    supplierName: 'Zuellig Pharma Corporation',
    prNumber: 'PR-2025-0387',
    modeOfProcurement: 'Public Bidding',
    totalAmount: 2850000,
    status: POStatus.Approved,
    createdAt: '2025-09-15T08:30:00.000Z',
    items: [
      { id: 'po-001-item-1', itemDescription: 'Paracetamol 500mg Tablet (Box of 100s)', quantity: 5000, unitCost: 45, totalCost: 225000 },
      { id: 'po-001-item-2', itemDescription: 'Amoxicillin 500mg Capsule (Box of 100s)', quantity: 3000, unitCost: 320, totalCost: 960000 },
      { id: 'po-001-item-3', itemDescription: 'Sodium Chloride 0.9% IV Fluid 1000ml', quantity: 4000, unitCost: 85, totalCost: 340000 },
      { id: 'po-001-item-4', itemDescription: 'Metformin 500mg Tablet (Box of 100s)', quantity: 2000, unitCost: 180, totalCost: 360000 },
      { id: 'po-001-item-5', itemDescription: 'Omeprazole 20mg Capsule (Box of 100s)', quantity: 1500, unitCost: 290, totalCost: 435000 },
      { id: 'po-001-item-6', itemDescription: 'Ringer Lactate IV Fluid 1000ml', quantity: 3000, unitCost: 95, totalCost: 285000 },
      { id: 'po-001-item-7', itemDescription: 'Ceftriaxone 1g Vial Injection', quantity: 800, unitCost: 356.25, totalCost: 285000 },
    ],
  },
  {
    id: 'po-002',
    poNumber: 'PO-2025-DRMC-0143',
    supplierName: 'Metro Drug Inc.',
    prNumber: 'PR-2025-0391',
    modeOfProcurement: 'SVP',
    totalAmount: 645000,
    status: POStatus.PendingBAC,
    createdAt: '2025-09-22T10:15:00.000Z',
    items: [
      { id: 'po-002-item-1', itemDescription: 'N95 Respirator Mask (Box of 20s)', quantity: 2000, unitCost: 125, totalCost: 250000 },
      { id: 'po-002-item-2', itemDescription: 'Surgical Gloves Size 7.5 (Box of 100s)', quantity: 1500, unitCost: 90, totalCost: 135000 },
      { id: 'po-002-item-3', itemDescription: 'Disposable Syringe 10ml (Box of 100s)', quantity: 1000, unitCost: 95, totalCost: 95000 },
      { id: 'po-002-item-4', itemDescription: 'Sterile Gauze Pad 4x4 (Box of 200s)', quantity: 1650, unitCost: 100, totalCost: 165000 },
    ],
  },
  {
    id: 'po-003',
    poNumber: 'PO-2025-DRMC-0144',
    supplierName: 'Unilab Laboratories',
    prNumber: 'PR-2025-0394',
    modeOfProcurement: 'Direct Contracting',
    totalAmount: 178500,
    status: POStatus.Approved,
    createdAt: '2025-09-28T14:00:00.000Z',
    items: [
      { id: 'po-003-item-1', itemDescription: 'Insulin Human Regular 100IU/ml (10ml Vial)', quantity: 350, unitCost: 510, totalCost: 178500 },
    ],
  },
  {
    id: 'po-004',
    poNumber: 'PO-2025-DRMC-0145',
    supplierName: 'ABC Medical Supplies Trading',
    prNumber: 'PR-2025-0402',
    modeOfProcurement: 'SVP',
    totalAmount: 312000,
    status: POStatus.Delivered,
    createdAt: '2025-08-05T09:00:00.000Z',
    items: [
      { id: 'po-004-item-1', itemDescription: 'Foley Catheter 16Fr (Box of 10s)', quantity: 1200, unitCost: 120, totalCost: 144000 },
      { id: 'po-004-item-2', itemDescription: 'IV Infusion Set with Drip Chamber (Box of 50s)', quantity: 840, unitCost: 200, totalCost: 168000 },
    ],
  },
  {
    id: 'po-005',
    poNumber: 'PO-2025-DRMC-0146',
    supplierName: 'Zuellig Pharma Corporation',
    prNumber: 'PR-2025-0410',
    modeOfProcurement: 'Public Bidding',
    totalAmount: 1890000,
    status: POStatus.PendingBAC,
    createdAt: '2025-10-02T11:30:00.000Z',
    items: [
      { id: 'po-005-item-1', itemDescription: 'Dextrose 5% Water IV Fluid 1000ml', quantity: 5000, unitCost: 78, totalCost: 390000 },
      { id: 'po-005-item-2', itemDescription: 'Cefuroxime 750mg Vial Injection', quantity: 2000, unitCost: 285, totalCost: 570000 },
      { id: 'po-005-item-3', itemDescription: 'Levofloxacin 500mg Tablet (Box of 100s)', quantity: 1800, unitCost: 520, totalCost: 936000 },
    ],
  },
  {
    id: 'po-006',
    poNumber: 'PO-2025-DRMC-0147',
    supplierName: 'Philippine Medical Distributors Corp.',
    prNumber: 'PR-2025-0415',
    modeOfProcurement: 'SVP',
    totalAmount: 92000,
    status: POStatus.Draft,
    createdAt: '2025-10-08T15:45:00.000Z',
    items: [
      { id: 'po-006-item-1', itemDescription: 'Digital Thermometer (Clinical Grade)', quantity: 400, unitCost: 230, totalCost: 92000 },
    ],
  },
  {
    id: 'po-007',
    poNumber: 'PO-2025-DRMC-0148',
    supplierName: 'MedExpress Distributors Inc.',
    prNumber: 'PR-2025-0418',
    modeOfProcurement: 'Public Bidding',
    totalAmount: 425000,
    status: POStatus.Cancelled,
    createdAt: '2025-07-18T08:00:00.000Z',
    items: [
      { id: 'po-007-item-1', itemDescription: 'Pulse Oximeter Fingertip (Unit)', quantity: 250, unitCost: 850, totalCost: 212500 },
      { id: 'po-007-item-2', itemDescription: 'Blood Pressure Cuff Adult (Aneroid)', quantity: 250, unitCost: 850, totalCost: 212500 },
    ],
  },
]

function delay<T>(value: T, ms = MOCK_DELAY): Promise<T> {
  return new Promise((resolve) => setTimeout(() => resolve(value), ms))
}

let nextId = seedData.length + 1

export class MockPurchaseOrderRepository implements IPurchaseOrderRepository {
  private store: RawPurchaseOrder[] = [...seedData]

  async getAll(): Promise<PurchaseOrder[]> {
    const sorted = [...this.store].sort(
      (a, b) => new Date(b.createdAt).getTime() - new Date(a.createdAt).getTime(),
    )
    return delay(sorted.map((r) => PurchaseOrderMapper.toDomain(r)))
  }

  async getById(id: string): Promise<PurchaseOrder | null> {
    const raw = this.store.find((r) => r.id === id)
    return delay(raw ? PurchaseOrderMapper.toDomain(raw) : null)
  }

  async create(dto: CreatePurchaseOrderDto): Promise<PurchaseOrder> {
    const id = `po-${String(nextId++).padStart(3, '0')}`
    const raw = PurchaseOrderMapper.fromDto(dto, id)
    this.store.push(raw)
    return delay(PurchaseOrderMapper.toDomain(raw))
  }

  async approve(id: string): Promise<PurchaseOrder> {
    const raw = this.store.find((r) => r.id === id)
    if (!raw) {
      throw new Error('Purchase order not found')
    }
    raw.status = POStatus.Approved
    return delay(PurchaseOrderMapper.toDomain(raw))
  }
}
