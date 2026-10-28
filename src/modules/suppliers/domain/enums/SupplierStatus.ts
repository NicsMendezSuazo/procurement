export enum SupplierStatus {
  Active = 'Active',
  Suspended = 'Suspended',
  Blacklisted = 'Blacklisted',
  Pending = 'Pending',
}

export const SupplierStatusLabels: Record<SupplierStatus, string> = {
  [SupplierStatus.Active]: 'Active',
  [SupplierStatus.Suspended]: 'Suspended',
  [SupplierStatus.Blacklisted]: 'Blacklisted',
  [SupplierStatus.Pending]: 'Pending Verification',
}

export type SupplierStatusVariant = 'gray' | 'blue' | 'amber' | 'emerald' | 'red'

export const SupplierStatusVariants: Record<SupplierStatus, SupplierStatusVariant> = {
  [SupplierStatus.Active]: 'emerald',
  [SupplierStatus.Suspended]: 'amber',
  [SupplierStatus.Blacklisted]: 'red',
  [SupplierStatus.Pending]: 'blue',
}
