import type { IDateParam, INumberParam, ISearchData, IStringParam } from '@/types/interfaces'

interface useFilterHelperProp {
  getStringParam: (name: string) => IStringParam
  getNumberParam: (name: string) => INumberParam
  getDateParam: (name: string) => IDateParam
  getValidatedSearchData: () => ISearchData
  resetFilter: () => void
}

export default function useFilterHelper(filter: ISearchData): useFilterHelperProp {
  function getStringParam(name: string) {
    return filter.StringParams.find((f) => f.Field == name)!
  }

  function getNumberParam(name: string) {
    return filter.NumberParams.find((f) => f.Field == name)!
  }

  function getDateParam(name: string) {
    return filter.DateParams.find((f) => f.Field == name)!
  }

  function getValidatedSearchData() {
    return {
      StringParams: filter.StringParams.filter((p) => p.Value),
      NumberParams: filter.NumberParams.filter((p) => p.Value !== null),
      DateParams: filter.DateParams.filter((p) => p.Value !== null),
    }
  }

  function resetFilter() {
    filter.StringParams.forEach((p) => (p.Value = ''))
    filter.NumberParams.forEach((p) => (p.Value = null))
    filter.DateParams.forEach((p) => (p.Value = null))
  }

  return { getStringParam, getNumberParam, getDateParam, getValidatedSearchData, resetFilter }
}
