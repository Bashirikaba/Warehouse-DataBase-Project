import Api from '@/apiProvider/api'
import { defaultSearchOpeartion } from '@/consts/searchOperations'
import type { IInvoice, ISearchData } from '@/types/interfaces'
import useFilterHelper from '@/hooks/useFilterHelper'
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
        Field: 'RouteType',
        Value: null,
        Operation: defaultSearchOpeartion.Value,
      },
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

  async function getAllInvoices() {
    const response: IInvoice[] = await Api.getEntity<IInvoice>('Invoices')

    Object.assign(invoices, response)
  }

  async function getInvoicesWithFilter() {
    const { getValidatedSearchData } = useFilterHelper(filter)
    const response: IInvoice[] = await Api.getEntity<IInvoice>('Invoices', getValidatedSearchData())

    console.log(response)

    invoices.length = 0
    Object.assign(invoices, response)
  }

  return { invoices, filter, getAllInvoices, getInvoicesWithFilter }
}
