export enum PRStatus {
  Draft = 'Draft',
  ForReview = 'ForReview',
  Endorsed = 'Endorsed',
  Converted = 'Converted',
  Rejected = 'Rejected',
}

export const PRStatusLabels: Record<PRStatus, string> = {
  [PRStatus.Draft]: 'Draft',
  [PRStatus.ForReview]: 'For Review',
  [PRStatus.Endorsed]: 'Endorsed',
  [PRStatus.Converted]: 'Converted to PO',
  [PRStatus.Rejected]: 'Rejected',
}

export type PRStatusVariant = 'gray' | 'blue' | 'amber' | 'emerald' | 'red'

export const PRStatusVariants: Record<PRStatus, PRStatusVariant> = {
  [PRStatus.Draft]: 'gray',
  [PRStatus.ForReview]: 'amber',
  [PRStatus.Endorsed]: 'blue',
  [PRStatus.Converted]: 'emerald',
  [PRStatus.Rejected]: 'red',
}
