
export interface Adapter<T> {
  adapt(toAdapt: any): T
}