import { defaultSearchOpeartion } from '@/consts/searchOperations'
import type {
  IDateParam,
  INumberParam,
  ISearchData,
  IStringParam,
  ITableConfigItem,
} from '@/types/interfaces'

interface useFilterHelperProp {
  initFilter: (config: ITableConfigItem[]) => ISearchData
  getStringParam: (filter: ISearchData, name: string) => IStringParam
  getNumberParam: (filter: ISearchData, name: string) => INumberParam
  getDateParam: (filter: ISearchData, name: string) => IDateParam
  getValidatedSearchData: (filter: ISearchData) => ISearchData
  resetFilter: (filter: ISearchData) => void
}

export default function useFilterHelper(): useFilterHelperProp {
  function initFilter(config: ITableConfigItem[]) {
    const filter: ISearchData = {
      StringParams: [],
      NumberParams: [],
      DateParams: [],
    }

    config.forEach((item) => {
      if (item.Type == 'string') {
        filter.StringParams.push({
          Field: item.Field,
          Value: '',
          Operation: defaultSearchOpeartion.Value,
        })
      }
      if (item.Type == 'number') {
        filter.NumberParams.push({
          Field: item.Field,
          Value: null,
          Operation: defaultSearchOpeartion.Value,
        })
      }
      if (item.Type == 'date') {
        filter.DateParams.push({
          Field: item.Field,
          Value: null,
          Operation: defaultSearchOpeartion.Value,
        })
      }
    })

    return filter
  }

  function getStringParam(filter: ISearchData, name: string) {
    return filter.StringParams.find((f) => f.Field == name)!
  }

  function getNumberParam(filter: ISearchData, name: string) {
    return filter.NumberParams.find((f) => f.Field == name)!
  }

  function getDateParam(filter: ISearchData, name: string) {
    return filter.DateParams.find((f) => f.Field == name)!
  }

  function getValidatedSearchData(filter: ISearchData) {
    return {
      StringParams: filter.StringParams.filter((p) => p.Value),
      NumberParams: filter.NumberParams.filter((p) => p.Value !== null),
      DateParams: filter.DateParams.filter((p) => p.Value !== null),
      SortExpression: filter.SortExpression,
    }
  }

  function resetFilter(filter: ISearchData) {
    filter.StringParams.forEach((p) => (p.Value = ''))
    filter.NumberParams.forEach((p) => (p.Value = null))
    filter.DateParams.forEach((p) => (p.Value = null))
  }

  return {
    initFilter,
    getStringParam,
    getNumberParam,
    getDateParam,
    getValidatedSearchData,
    resetFilter,
  }
}
