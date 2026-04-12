import type { SearchData } from './types/interfaces'
import type { Endpoints, Services } from './types/types'

export default class Api {
  private static readonly baseUrl: string = 'http://localhost:5129/api'
  private static options: RequestInit = {
    method: 'POST',
    mode: 'cors',
    headers: { 'Content-Type': 'application/json', 'Response-Type': 'application/json' },
    body: '{}',
  }

  private static service: Services
  private static endpoint: Endpoints

  static async getEntity<T>(service: Services, params?: SearchData): Promise<T[]> {
    let filledBody: RequestInit
    let response: Response

    this.endpoint = 'Get'
    this.service = service

    if (params) {
      filledBody = { ...this.options, body: JSON.stringify(params) }
      response = await fetch(this.buildQuery(), filledBody)
    } else {
      response = await fetch(this.buildQuery(), this.options)
    }

    return (await response.json()) as T[]
  }

  private static buildQuery(): string {
    return `${this.baseUrl}/${this.service}/${this.endpoint}`
  }
}
