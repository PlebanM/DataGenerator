import { Component, OnInit } from '@angular/core';
import { ColumnTypesGetterService } from '../services/column-types-getter.service';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.css']
})
export class TableComponent implements OnInit {

  constructor(private columnTypeGetter: ColumnTypesGetterService) { }

  getColumnTypes(): void {
    this.columnTypeGetter.getColumnTypes().subscribe(res => {
      console.log(res);
    });
  }

  ngOnInit() {
  }

}
