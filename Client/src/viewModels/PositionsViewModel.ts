import Api from '@/apiProvider/api'
import type { Position } from '@/apiProvider/types/interfaces'
import { reactive, type Reactive } from 'vue'

export function usePositionsOperations() {
  const positions: Reactive<Position[]> = reactive([])

  async function getAllPositions() {
    const response: Position[] = await Api.getEntity<Position>('Positions')

    Object.assign(positions, response)
    console.log(positions)
  }

  return { positions, getAllPositions }
}
