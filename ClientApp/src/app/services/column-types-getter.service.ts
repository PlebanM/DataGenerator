import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { ColumnTypeAdapter, ColumnType } from '../models/column-type';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ColumnTypesGetterService {

  private optionsUrl = "https://localhost:44361/api/generator/options";

  constructor(private http: HttpClient, private columnTypeAdapter: ColumnTypeAdapter) { }

  getColumnTypes(): Observable<ColumnType[]> {
    return this.http.get(this.optionsUrl).pipe(map((data: any[]) => data.map(item => this.columnTypeAdapter.adapt(item))));
  }
}
