import { SearchOperationsEnum } from '../types/enums'
import { type ISearchOperationsItem } from '../types/interfaces'

export const numberSearchOperations: ISearchOperationsItem[] = [
  { Value: SearchOperationsEnum.MoreThan, Name: 'Больше' },
  { Value: SearchOperationsEnum.LessThan, Name: 'Меньше' },
  { Value: SearchOperationsEnum.Equal, Name: 'Равно' },
]

export const stringSearchOperations: ISearchOperationsItem[] = [
  { Value: SearchOperationsEnum.Equal, Name: 'Равно' },
  { Value: SearchOperationsEnum.Like, Name: 'Содержит' },
]

export const dateSearchOperations: ISearchOperationsItem[] = [
  { Value: SearchOperationsEnum.MoreThan, Name: 'Больше' },
  { Value: SearchOperationsEnum.LessThan, Name: 'Меньше' },
  { Value: SearchOperationsEnum.Equal, Name: 'Равно' },
]

export const defaultSearchOpeartion: ISearchOperationsItem = numberSearchOperations[2]!
