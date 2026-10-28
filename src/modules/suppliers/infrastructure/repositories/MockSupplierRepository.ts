import type { Supplier } from '../../domain/models/Supplier'
import { SupplierStatus } from '../../domain/enums/SupplierStatus'

const MOCK_DELAY = 400

const seedData: Supplier[] = [
  { id: 'sup-001', supplierName: 'Zuellig Pharma Corporation', category: 'Pharmaceuticals', contactPerson: 'Mr. Antonio Lim', contactNumber: '+63 2 8888-1234', email: 'govt.sales@zuelligpharma.com', address: '19F Sun Life Centre, 5th Ave, Fort Bonifacio, Taguig', philgepsRefId: 'PHGEPS-2018-000234', status: SupplierStatus.Active, totalContracts: 24, totalContractValue: 12500000, registeredDate: '2018-03-15' },
  { id: 'sup-002', supplierName: 'Metro Drug Inc.', category: 'Medical Supplies', contactPerson: 'Ms. Patricia Ng', contactNumber: '+63 2 8555-6789', email: 'procurement@metrodrug.com.ph', address: 'Gregoria de Jesus St, Caloocan City', philgepsRefId: 'PHGEPS-2019-000451', status: SupplierStatus.Active, totalContracts: 18, totalContractValue: 6800000, registeredDate: '2019-01-20' },
  { id: 'sup-003', supplierName: 'Unilab Laboratories', category: 'Pharmaceuticals', contactPerson: 'Dr. Fernando Reyes', contactNumber: '+63 2 8777-9012', email: 'hospital.supply@unilab.com', address: 'Unilab Corporate Centre, Mandaluyong City', philgepsRefId: 'PHGEPS-2017-000089', status: SupplierStatus.Active, totalContracts: 32, totalContractValue: 18900000, registeredDate: '2017-06-10' },
  { id: 'sup-004', supplierName: 'ABC Medical Supplies Trading', category: 'Medical Equipment', contactPerson: 'Mr. Eduardo Santos', contactNumber: '+63 82 222-3456', email: 'sales@abcmedical.com.ph', address: 'J.P. Laurel Ave, Davao City', philgepsRefId: 'PHGEPS-2020-000678', status: SupplierStatus.Active, totalContracts: 9, totalContractValue: 2100000, registeredDate: '2020-08-05' },
  { id: 'sup-005', supplierName: 'Philippine Medical Distributors Corp.', category: 'Medical Supplies', contactPerson: 'Ms. Catherine Torres', contactNumber: '+63 2 8999-1122', email: 'orders@pmdc.com.ph', address: '31 Scout Rallos St, Quezon City', philgepsRefId: 'PHGEPS-2021-000890', status: SupplierStatus.Pending, totalContracts: 3, totalContractValue: 450000, registeredDate: '2025-09-01' },
  { id: 'sup-006', supplierName: 'MedExpress Distributors Inc.', category: 'Medical Equipment', contactPerson: 'Mr. Roberto Garcia', contactNumber: '+63 82 333-4567', email: 'info@medexpress.com.ph', address: 'Bajada, Davao City', philgepsRefId: 'PHGEPS-2020-000543', status: SupplierStatus.Suspended, totalContracts: 7, totalContractValue: 1850000, registeredDate: '2020-02-15' },
  { id: 'sup-007', supplierName: 'HealthCare Essentials Trading', category: 'Consumables', contactPerson: 'Ms. Liza Macapagal', contactNumber: '+63 2 8444-5566', email: 'contact@hcet.com.ph', address: 'Sen. Gil Puyat Ave, Makati City', philgepsRefId: 'PHGEPS-2022-001234', status: SupplierStatus.Active, totalContracts: 12, totalContractValue: 3200000, registeredDate: '2022-04-20' },
  { id: 'sup-008', supplierName: 'Global Pharma Imports', category: 'Pharmaceuticals', contactPerson: 'Mr. James Wong', contactNumber: '+63 2 8666-7788', email: 'imports@globalpharma.ph', address: 'Port Area, Manila', philgepsRefId: 'PHGEPS-2023-001567', status: SupplierStatus.Blacklisted, totalContracts: 2, totalContractValue: 890000, registeredDate: '2023-01-10' },
]

function delay<T>(value: T, ms = MOCK_DELAY): Promise<T> {
  return new Promise((resolve) => setTimeout(() => resolve(value), ms))
}

export class MockSupplierRepository {
  private store: Supplier[] = [...seedData]

  async getAll(): Promise<Supplier[]> {
    return delay([...this.store])
  }
}
