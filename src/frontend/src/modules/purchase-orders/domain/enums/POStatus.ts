export enum POStatus {
  Draft = 'Draft',
  PendingBAC = 'PendingBAC',
  Approved = 'Approved',
  Delivered = 'Delivered',
  Cancelled = 'Cancelled',
}

export const POStatusLabels: Record<POStatus, string> = {
  [POStatus.Draft]: 'Draft',
  [POStatus.PendingBAC]: 'Pending BAC',
  [POStatus.Approved]: 'Approved',
  [POStatus.Delivered]: 'Delivered',
  [POStatus.Cancelled]: 'Cancelled',
}

export type POStatusVariant = 'gray' | 'blue' | 'amber' | 'emerald' | 'red'

export const POStatusVariants: Record<POStatus, POStatusVariant> = {
  [POStatus.Draft]: 'gray',
  [POStatus.PendingBAC]: 'amber',
  [POStatus.Approved]: 'emerald',
  [POStatus.Delivered]: 'blue',
  [POStatus.Cancelled]: 'red',
}

export type ModeOfProcurement = 'Public Bidding' | 'SVP' | 'Direct Contracting'

export const ModeOfProcurementLabels: Record<ModeOfProcurement, string> = {
  'Public Bidding': 'Public Bidding',
  SVP: 'Small Value Procurement',
  'Direct Contracting': 'Direct Contracting',
}
