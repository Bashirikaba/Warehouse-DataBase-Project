export type Endpoint = 'Add' | 'Get' | 'Update' | 'Delete'

export type Service =
  | 'Balances'
  | 'Goods'
  | 'Invoices'
  | 'Positions'
  | 'Staff'
  | 'Warehouses'
  | 'Reports'

export type Report = 'GetReorderGoods' | 'GetStaffPerformance' | 'GetWarehousePeriod'
