import type { PurchaseRequest } from '../../domain/models/PurchaseRequest'
import { PRStatus } from '../../domain/enums/PRStatus'

const MOCK_DELAY = 400

const seedData: PurchaseRequest[] = [
  { id: 'pr-001', prNumber: 'PR-2025-0420', requestingDepartment: 'Emergency Department', requestedBy: 'Dr. Ramos, J.', itemSummary: 'Emergency trauma kits and wound care supplies', itemCount: 12, estimatedAmount: 185000, status: PRStatus.ForReview, priority: 'Urgent', createdAt: '2025-10-20T08:00:00.000Z', neededBy: '2025-11-05' },
  { id: 'pr-002', prNumber: 'PR-2025-0421', requestingDepartment: 'Pharmacy', requestedBy: 'Pharm. Cruz, M.', itemSummary: 'Antibiotics and IV fluids restocking', itemCount: 8, estimatedAmount: 420000, status: PRStatus.Endorsed, priority: 'High', createdAt: '2025-10-18T10:30:00.000Z', neededBy: '2025-11-10' },
  { id: 'pr-003', prNumber: 'PR-2025-0422', requestingDepartment: 'Pediatrics', requestedBy: 'Dr. Santos, L.', itemSummary: 'Pediatric medicines and vitamins', itemCount: 15, estimatedAmount: 95000, status: PRStatus.ForReview, priority: 'Medium', createdAt: '2025-10-22T14:00:00.000Z', neededBy: '2025-11-15' },
  { id: 'pr-004', prNumber: 'PR-2025-0423', requestingDepartment: 'Surgery Department', requestedBy: 'Dr. Tan, R.', itemSummary: 'Surgical gloves, sutures, and sterile drapes', itemCount: 20, estimatedAmount: 310000, status: PRStatus.Converted, priority: 'High', createdAt: '2025-10-10T09:00:00.000Z', neededBy: '2025-10-25' },
  { id: 'pr-005', prNumber: 'PR-2025-0424', requestingDepartment: 'Laboratory', requestedBy: 'Mr. Aquino, P.', itemSummary: 'Blood collection tubes and reagents', itemCount: 6, estimatedAmount: 75000, status: PRStatus.Draft, priority: 'Low', createdAt: '2025-10-24T11:00:00.000Z', neededBy: '2025-11-20' },
  { id: 'pr-006', prNumber: 'PR-2025-0425', requestingDepartment: 'ICU', requestedBy: 'Dr. Reyes, A.', itemSummary: 'Ventilator circuits and ICU consumables', itemCount: 10, estimatedAmount: 560000, status: PRStatus.ForReview, priority: 'Urgent', createdAt: '2025-10-23T07:30:00.000Z', neededBy: '2025-11-01' },
  { id: 'pr-007', prNumber: 'PR-2025-0426', requestingDepartment: 'Radiology', requestedBy: 'Mr. Dela Cruz, B.', itemSummary: 'X-ray films and contrast media', itemCount: 7, estimatedAmount: 220000, status: PRStatus.Rejected, priority: 'Medium', createdAt: '2025-10-15T13:00:00.000Z', neededBy: '2025-11-01' },
  { id: 'pr-008', prNumber: 'PR-2025-0427', requestingDepartment: 'OB-GYN', requestedBy: 'Dr. Villanueva, C.', itemSummary: 'Delivery room supplies and newborn care kits', itemCount: 14, estimatedAmount: 165000, status: PRStatus.Endorsed, priority: 'High', createdAt: '2025-10-21T15:00:00.000Z', neededBy: '2025-11-12' },
]

function delay<T>(value: T, ms = MOCK_DELAY): Promise<T> {
  return new Promise((resolve) => setTimeout(() => resolve(value), ms))
}

export class MockPurchaseRequestRepository {
  private store: PurchaseRequest[] = [...seedData]

  async getAll(): Promise<PurchaseRequest[]> {
    const sorted = [...this.store].sort(
      (a, b) => new Date(b.createdAt).getTime() - new Date(a.createdAt).getTime(),
    )
    return delay(sorted)
  }
}
