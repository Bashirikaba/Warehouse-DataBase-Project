import type { ISearchData, ISearchDataDto } from '../types/interfaces'
import type { Endpoint, Service } from '../types/types'

export default class Api {
  private static readonly baseUrl: string = 'http://localhost:5129/api'
  private static options: RequestInit = {
    method: 'POST',
    mode: 'cors',
    headers: { 'Content-Type': 'application/json', 'Response-Type': 'application/json' },
    body: '{}',
  }

  private static service: Service
  private static endpoint: Endpoint

  static setService(service: Service): void {
    this.service = service
  }

  static async getEntity<T>(params?: ISearchData): Promise<T[]> {
    let filledBody: RequestInit
    let response: Response

    this.endpoint = 'Get'

    if (params) {
      const dto: ISearchDataDto = { dto: params }
      filledBody = { ...this.options, body: JSON.stringify(dto) }
      response = await fetch(this.buildQuery(), filledBody)
    } else {
      response = await fetch(this.buildQuery(), this.options)
    }

    return (await response.json()) as T[]
  }

  static async deleteEntity(id: number) {
    this.endpoint = 'Delete'

    const filledBody: RequestInit = { ...this.options, body: JSON.stringify({ id: id }) }
    await fetch(this.buildQuery(), filledBody)
  }

  private static buildQuery(): string {
    return `${this.baseUrl}/${this.service}/${this.endpoint}`
  }
}
