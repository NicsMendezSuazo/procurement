export interface HttpResponse<T = unknown> {
  data: T
  status: number
  message?: string
}

export interface HttpClient {
  get<T = unknown>(url: string, config?: Record<string, unknown>): Promise<HttpResponse<T>>
  post<T = unknown>(url: string, body?: unknown, config?: Record<string, unknown>): Promise<HttpResponse<T>>
  patch<T = unknown>(url: string, body?: unknown, config?: Record<string, unknown>): Promise<HttpResponse<T>>
  delete<T = unknown>(url: string, config?: Record<string, unknown>): Promise<HttpResponse<T>>
}
