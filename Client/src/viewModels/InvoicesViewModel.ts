import Api from '@/apiProvider/api'
import { defaultSearchOpeartion } from '@/consts/searchOperations'
import type { IInvoice, ISearchData } from '@/types/interfaces'
import { reactive, type Reactive } from 'vue'

export function useInvoicesActions() {
  const invoices: Reactive<IInvoice[]> = reactive([])

  const filter: Reactive<ISearchData> = reactive({
    StringParams: [
      {
        Field: 'WarehouseName',
        Value: '',
        Operation: defaultSearchOpeartion.Value,
      },
      {
        Field: 'GoodNomenclatureNumber',
        Value: '',
        Operation: defaultSearchOpeartion.Value,
      },
      {
        Field: 'InvoiceNumber',
        Value: '',
        Operation: defaultSearchOpeartion.Value,
      },
    ],
    NumberParams: [
      {
        Field: 'Quantity',
        Value: null,
        Operation: defaultSearchOpeartion.Value,
      },
      {
        Field: 'Cost',
        Value: null,
        Operation: defaultSearchOpeartion.Value,
      },
    ],
    DateParams: [
      {
        Field: 'Date',
        Value: null,
        Operation: defaultSearchOpeartion.Value,
      },
    ],
  })

  async function getAllPositions() {
    const response: IPosition[] = await Api.getEntity<IPosition>('Positions')

    Object.assign(positions, response)
  }

  //  async function getPositionsWithFilter(searchData: SearchData) {}

  return { positions, filter, getAllPositions }
}
