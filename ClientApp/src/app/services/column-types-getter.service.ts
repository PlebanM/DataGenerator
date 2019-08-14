import { Injectable, OnInit } from '@angular/core';
import { Observable, BehaviorSubject } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { ColumnTypeAdapter, ColumnType } from '../models/column-type';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ColumnTypesGetterService {

  private optionsUrl = "https://localhost:44361/api/generator/options";
  private columnTypesObs = new Observable<Array<ColumnType>>();

  constructor(private http: HttpClient, private columnTypeAdapter: ColumnTypeAdapter) {
    this.columnTypesObs = this.downloadColumnTypes();
  }

  downloadColumnTypes(): Observable<ColumnType[]> {
    return this.http.get(this.optionsUrl).pipe(map((data: any[]) => data.map(item => this.columnTypeAdapter.adapt(item))));
  }

  getColumnTypes(): Observable<ColumnType[]> {
    return this.columnTypesObs;
  }
}
