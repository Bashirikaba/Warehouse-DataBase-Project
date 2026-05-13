import type { ITableConfigItem } from '@/types/interfaces'

export const balancesConfig: ITableConfigItem[] = [
  {
    Field: 'Id',
    Label: 'Id',
    Type: 'number',
    Hidden: true,
  },
  {
    Field: 'Warehouse.Name',
    Label: 'Название склада',
    Type: 'string',
    NestedEntity: 'Warehouses',
  },
  {
    Field: 'Good.NomenclatureNumber',
    Label: 'Ном.№ товара',
    Type: 'string',
    NestedEntity: 'Goods',
  },
  {
    Field: 'Quantity',
    Label: 'Количество товара',
    Type: 'number',
  },
]

export const goodsConfig: ITableConfigItem[] = [
  {
    Field: 'Id',
    Label: 'Id',
    Type: 'number',
    Hidden: true,
  },
  {
    Field: 'Code',
    Label: 'Код товара',
    Type: 'string',
  },
  {
    Field: 'NomenclatureNumber',
    Label: 'Ном.№ товара',
    Type: 'string',
  },
  {
    Field: 'Name',
    Label: 'Название',
    Type: 'string',
  },
  {
    Field: 'Price',
    Label: 'Стоимость',
    Type: 'number',
    Numeric: true,
  },
]

export const invoicesConfig: ITableConfigItem[] = [
  {
    Field: 'Id',
    Label: 'Id',
    Type: 'number',
    Hidden: true,
  },
  {
    Field: 'Warehouse.Name',
    Label: 'Название склада',
    Type: 'string',
    NestedEntity: 'Warehouses',
  },
  {
    Field: 'Good.NomenclatureNumber',
    Label: 'Ном.№ товара',
    Type: 'string',
    NestedEntity: 'Goods',
  },
  {
    Field: 'InvoiceNumber',
    Label: 'Номер накладной',
    Type: 'string',
  },
  {
    Field: 'Date',
    Label: 'Дата',
    Type: 'date',
  },
  {
    Field: 'RouteType',
    Label: 'Маршрут',
    Type: 'number',
    Enum: true,
  },
  {
    Field: 'Quantity',
    Label: 'Количество товара',
    Type: 'number',
  },
  {
    Field: 'Cost',
    Label: 'Стоимость товара',
    Type: 'number',
    Numeric: true,
  },
]

export const staffConfig: ITableConfigItem[] = [
  {
    Field: 'Id',
    Label: 'Id',
    Type: 'number',
    Hidden: true,
  },
  {
    Field: 'Warehouse.Name',
    Label: 'Название склада',
    Type: 'string',
    NestedEntity: 'Warehouses',
  },
  {
    Field: 'Position.Name',
    Label: 'Название должности',
    Type: 'string',
    NestedEntity: 'Positions',
  },
  {
    Field: 'FullName',
    Label: 'ФИО',
    Type: 'string',
  },
  {
    Field: 'TIN',
    Label: 'ИНН',
    Type: 'string',
  },
]

export const positionsConfig: ITableConfigItem[] = [
  {
    Field: 'Id',
    Label: 'Id',
    Type: 'number',
    Hidden: true,
  },
  {
    Field: 'Name',
    Label: 'Название должности',
    Type: 'string',
  },
]

export const warehousesConfig: ITableConfigItem[] = [
  {
    Field: 'Id',
    Label: 'Id',
    Type: 'number',
    Hidden: true,
  },
  {
    Field: 'Name',
    Label: 'Название склада',
    Type: 'string',
  },
]

export const reorderGoodsReportConfig: ITableConfigItem[] = [
  {
    Field: 'Name',
    Label: 'Название товара',
    Type: 'string',
  },
  {
    Field: 'Code',
    Label: 'Код товара',
    Type: 'string',
  },
  {
    Field: 'WarehouseName',
    Label: 'Склад',
    Type: 'string',
  },
  {
    Field: 'Quantity',
    Label: 'Кол-во товара',
    Type: 'number',
  },
  {
    Field: 'SoldLast30Days',
    Label: 'Продано за последние 30 дней',
    Type: 'number',
  },
]

export const staffPerformanceReportConfig: ITableConfigItem[] = [
  {
    Field: 'FullName',
    Label: 'ФИО',
    Type: 'string',
  },
  {
    Field: 'Position',
    Label: 'Название должности',
    Type: 'string',
  },
  {
    Field: 'WarehouseName',
    Label: 'Склад',
    Type: 'string',
  },
  {
    Field: 'DocumentsProcessed',
    Label: 'Документов проведено',
    Type: 'number',
  },
  {
    Field: 'UniqueGoodsHandled',
    Label: 'Кол-во типов товаров',
    Type: 'number',
  },
  {
    Field: 'TotalItemsProcessed',
    Label: 'Всего товаров проведено',
    Type: 'number',
  },
  {
    Field: 'TotalValueProcessed',
    Label: 'Общая сумма',
    Type: 'number',
  },
  {
    Field: 'LastWorkDate',
    Label: 'Продано за последние 30 дней',
    Type: 'number',
  },
]

export const warehousePeriodReportConfig: ITableConfigItem[] = [
  {
    Field: 'WarehouseName',
    Label: 'Склад',
    Type: 'string',
  },
  {
    Field: 'Route',
    Label: 'Маршрут',
    Type: 'string',
  },
  {
    Field: 'InvoicesCount',
    Label: 'Кол-во извещений',
    Type: 'number',
  },
  {
    Field: 'UniqueGoods',
    Label: 'Кол-во типов товаров',
    Type: 'number',
  },
  {
    Field: 'TotalQuantity',
    Label: 'Уникальные товары',
    Type: 'number',
  },
  {
    Field: 'TotalSum',
    Label: 'Общая сумма',
    Type: 'number',
  },
  {
    Field: 'FirstDate',
    Label: 'Начало периода',
    Type: 'date',
  },
  {
    Field: 'LastDate',
    Label: 'Конец периода',
    Type: 'date',
  },
]
