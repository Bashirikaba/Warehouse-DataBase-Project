import type { SearchOperationsEnum } from './enums'

export interface IEntity {
  Id?: number
}

export interface IReport {
  WarehouseName: string
}

export interface IBalance extends IEntity {
  Id?: number
  WarehouseName: number
  GoodNomenclatureNumber: number
  Quantity: number
}

export interface IGood extends IEntity {
  Id?: number
  Code: string
  NomenclatureNumber: string
  Name: string
  Price: number
}

export interface IInvoice extends IEntity {
  Id?: number
  WarehouseName: string
  GoodNomenclatureNumber: string
  InvoiceNumber: string
  Date: Date
  RouteType: number
  Quantity: number
  Cost: number
}

export interface IPosition extends IEntity {
  Id?: number
  Name: string
}

export interface IStaff extends IEntity {
  Id?: number
  WarehouseName: string
  PositionName: string
  FullName: string
  TIN: string
}

export interface IWarehouse extends IEntity {
  Id?: number
  ManagerTIN: string
  Name: string
}

export interface IReorderGoodsReport extends IReport {
  Name: string
  Code: string
  WarehouseName: string
  Quantity: number
  SoldLast30Days: number
}

export interface IStaffPerformanceReport extends IReport {
  FullName: string
  Position: string
  WarehouseName: string
  DocumentProcessed: number
  UniqueGoodsHandled: number
  TotalItemsProcessed: number
  TotalValueProcessed: number
  LastWorkDate: Date
}

export interface IWarehousePeriodReport extends IReport {
  WarehouseName: string
  Route: number
  InvoicesCount: number
  UniqueGoods: number
  TotalQuantity: number
  TotalSum: number
  FirstDate: Date
  LastDate: Date
}

export interface ISearchDataDto {
  dto: ISearchData
}

export interface ISearchData {
  StringParams: IStringParam[]
  NumberParams: INumberParam[]
  DateParams: IDateParam[]
}

export interface IDateParam {
  Field: string
  Value: Date | null
  Operation: SearchOperationsEnum
}

export interface INumberParam {
  Field: string
  Value: number | null
  Operation: SearchOperationsEnum
}

export interface IStringParam {
  Field: string
  Value: string
  Operation: SearchOperationsEnum
}

export interface ISearchOperationsItem {
  Value: SearchOperationsEnum
  Name: string
}

export interface ITableConfigItem {
  Field: string
  Label: string
  Type: string
  Hidden?: boolean
}
