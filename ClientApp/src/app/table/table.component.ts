import { Component, OnInit, ViewChild, ViewContainerRef, ComponentFactoryResolver, ComponentRef, EventEmitter } from '@angular/core';
import { ColumnTypesGetterService } from '../services/column-types-getter.service';
import { ColumnType } from '../models/column-type';
import { ColumnComponent } from '../column/column.component';
import { FormArray, FormGroup, FormControl } from '@angular/forms';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.css']
})
export class TableComponent implements OnInit {

  _ref: any;
  deleteEvent = new EventEmitter<ComponentRef<TableComponent>>();

  columnTypes: Array<ColumnType>;
  columns: Array<ComponentRef<ColumnComponent>> = [];

  tableGroup: FormGroup = new FormGroup({
    name: new FormControl(),
    rowCount: new FormControl(),
    columnStructures: new FormArray([])
  });

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
    let instance = columnComponent.instance;
    instance.deleteEvent.subscribe(this.onColumnDelete.bind(this));
    instance.columnTypes = this.columnTypes;
    instance._ref = columnComponent;
    (<FormArray>this.tableGroup.get("columnStructures")).push(instance.columnGroup);
    this.columns.push(columnComponent);
    //console.log(this.tableGroup);
  }

  onColumnDelete(ref: ComponentRef<ColumnComponent>) {
    //console.log("before: ");
    //console.log(this.tableGroup);
    this.columns = this.columns.filter(elem => elem != ref);
    let formArray = <FormArray>this.tableGroup.get("columnStructures");
    formArray.removeAt(formArray.value.findIndex(group => group == ref.instance.columnGroup))
    //console.log("After: ");
    //console.log(this.tableGroup);
  }

  removeObject(): void {
    this.deleteEvent.emit(this._ref);
    this._ref.destroy();
  }

}
