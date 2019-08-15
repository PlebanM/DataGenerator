import { Component, OnInit } from '@angular/core';
import { ColumnTypesGetterService } from '../services/column-types-getter.service';
import { ColumnType } from '../models/column-type';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.css']
})
export class TableComponent implements OnInit {

  columnTypes: Array<ColumnType>;

  constructor(private columnTypeGetter: ColumnTypesGetterService) { }

  ngOnInit() {
    this.columnTypeGetter.getColumnTypes().subscribe(res => {
      this.columnTypes = res;
    });
  }

}
