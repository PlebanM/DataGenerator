import { Component, OnInit, ViewChild, ViewContainerRef, ComponentFactoryResolver } from '@angular/core';
import { ColumnTypesGetterService } from '../services/column-types-getter.service';
import { ColumnType } from '../models/column-type';
import { ColumnComponent } from '../column/column.component';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.css']
})
export class TableComponent implements OnInit {

  columnTypes: Array<ColumnType>;

  @ViewChild('columns', { static: true, read: ViewContainerRef })
  container: ViewContainerRef;

  constructor(private columnTypeGetter: ColumnTypesGetterService, private crf: ComponentFactoryResolver) { }

  ngOnInit() {
    this.columnTypeGetter.getColumnTypes().subscribe(res => {
      this.columnTypes = res;
    });
  }

  addColumn(): void {
    let column = this.crf.resolveComponentFactory(ColumnComponent);
    let columnComponent = this.container.createComponent(column);
    columnComponent.instance.columnTypes = this.columnTypes;
    columnComponent.instance._ref = columnComponent;
  }

}
