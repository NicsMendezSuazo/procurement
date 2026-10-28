import type { PRStatus } from '../enums/PRStatus'

export interface PurchaseRequest {
  id: string
  prNumber: string
  requestingDepartment: string
  requestedBy: string
  itemSummary: string
  itemCount: number
  estimatedAmount: number
  status: PRStatus
  priority: 'Low' | 'Medium' | 'High' | 'Urgent'
  createdAt: string
  neededBy: string
}
