import type { RouteTypeEnum, SearchOperationsEnum } from './enums'

export interface IBalance {
  Id?: number
  WarehouseName: number
  GoodNomenclatureNumber: number
  Quantity: number
}

export interface IGood {
  Id?: number
  Code: string
  NomenclatureNumber: string
  Name: string
  Price: number
}

export interface IInvoice {
  Id?: number
  WarehouseName: string
  GoodNomenclatureNumber: string
  InvoiceNumber: string
  Date: Date
  RouteType: RouteTypeEnum
  Quantity: number
  Cost: number
}

export interface IPosition {
  id?: number
  name: string
}

export interface IStaff {
  Id?: number
  WarehouseName: string
  PositionName: string
  FullName: string
  TIN: string
}

export interface IWarehouse {
  Id?: number
  ManagerTIN: string
  Name: string
}

export interface IReorderGoodsReport {
  Name: string
  Code: string
  WarehouseName: string
  Quantity: number
  SoldLast30Days: number
}

export interface IStaffPerformanceReport {
  FullName: string
  Position: string
  WarehouseName: string
  DocumentProcessed: number
  UniqueGoodsHandled: number
  TotalItemsProcessed: number
  TotalValueProcessed: number
  LastWorkDate: Date
}

export interface IWarehousePeriodReport {
  WarehouseName: string
  Route: RouteTypeEnum
  InvoicesCount: number
  UniqueGoods: number
  TotalQuantity: number
  TotalSum: number
  FirstDate: Date
  LastDate: Date
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

export interface IFilterConfigItem {
  Field: string
  Label: string
  Type: string
}
