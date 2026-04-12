import type { RouteType, SearchOperations } from './enums'

export interface Balance {
  Id?: number
  WarehouseName: number
  GoodNomenclatureNumber: number
  Quantity: number
}

export interface Good {
  Id?: number
  Code: string
  NomenclatureNumber: string
  Name: string
  Price: number
}

export interface Invoice {
  Id?: number
  WarehouseName: string
  GoodNomenclatureNumber: string
  InvoiceNumber: string
  Date: Date
  RouteType: RouteType
  Quantity: number
  Cost: number
}

export interface Position {
  id?: number
  name: string
}

export interface Staff {
  Id?: number
  WarehouseName: string
  PositionName: string
  FullName: string
  TIN: string
}

export interface Warehouse {
  Id?: number
  ManagerTIN: string
  Name: string
}

export interface ReorderGoodsReport {
  Name: string
  Code: string
  WarehouseName: string
  Quantity: number
  SoldLast30Days: number
}

export interface StaffPerformanceReport {
  FullName: string
  Position: string
  WarehouseName: string
  DocumentProcessed: number
  UniqueGoodsHandled: number
  TotalItemsProcessed: number
  TotalValueProcessed: number
  LastWorkDate: Date
}

export interface WarehousePeriodReport {
  WarehouseName: string
  Route: RouteType
  InvoicesCount: number
  UniqueGoods: number
  TotalQuantity: number
  TotalSum: number
  FirstDate: Date
  LastDate: Date
}

export interface SearchData {
  StringParams: StringParam[]
  NumberParams: NumberParam[]
  DateParams: DateParam[]
}

export interface DateParam {
  Field: string
  Value: Date
  Operation: SearchOperations
}

export interface NumberParam {
  Field: string
  Value: number
  Operation: SearchOperations
}

export interface StringParam {
  Field: string
  Value: string
  Operation: SearchOperations
}
