import Api from '@/apiProvider/api'
import { defaultSearchOpeartion } from '@/consts/searchOperations'
import type { IPosition, ISearchData } from '@/types/interfaces'
import { reactive, type Reactive } from 'vue'

export function usePositionsActions() {
  const positions: Reactive<IPosition[]> = reactive([])

  const filter: Reactive<ISearchData> = reactive({
    StringParams: [
      {
        Field: 'Name',
        Value: '',
        Operation: defaultSearchOpeartion.Value,
      },
    ],
    NumberParams: [],
    DateParams: [],
  })

  async function getAllPositions() {
    const response: IPosition[] = await Api.getEntity<IPosition>('Positions')

    Object.assign(positions, response)
  }

  //  async function getPositionsWithFilter(searchData: SearchData) {}

  return { positions, filter, getAllPositions }
}
