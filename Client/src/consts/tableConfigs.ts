import type { ITableConfigItem } from '@/types/interfaces'

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

export const invoicesConfig: ITableConfigItem[] = [
  {
    Field: 'Id',
    Label: 'Id',
    Type: 'number',
    Hidden: true,
  },
  {
    Field: 'WarehouseName',
    Label: 'Название склада',
    Type: 'string',
  },
  {
    Field: 'GoodNomenclatureNumber',
    Label: 'Ном.№ товара',
    Type: 'string',
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
  },
]
