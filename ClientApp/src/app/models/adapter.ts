
export interface Adapter<T> {
  adapt(columnTypes: any): T
}