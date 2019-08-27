import { Component, OnInit, ViewContainerRef, ViewChild, ComponentRef, ComponentFactoryResolver } from '@angular/core';
import { TableComponent } from '../table/table.component';
import { FormArray, FormControl, FormGroup } from '@angular/forms';
import { TableInputAdapter } from '../models/input-representations/table-input';
import { TableDataFormValidatorProviderService } from '../services/table-data-form-validator-provider.service';

@Component({
  selector: 'app-table-data-form',
  templateUrl: './table-data-form.component.html',
  styleUrls: ['./table-data-form.component.css']
})
export class TableDataFormComponent implements OnInit {

  @ViewChild('tables', { static: true, read: ViewContainerRef })
  container: ViewContainerRef;

  tables: Array<ComponentRef<TableComponent>> = [];
  tablesFormArray: FormArray;

  constructor(private cfr: ComponentFactoryResolver,
    private tia: TableInputAdapter,
    private tableDataFormValidatorProvider: TableDataFormValidatorProviderService) {
    this.tablesFormArray = new FormArray([], tableDataFormValidatorProvider.getForTables())
  }

  ngOnInit() {
  }

  addTable(): void {
    let tableFactory = this.cfr.resolveComponentFactory(TableComponent);
    let table = this.container.createComponent(tableFactory);
    table.instance._ref = table;
    table.instance.deleteEvent.subscribe(this.onTableDelete.bind(this));
    this.tablesFormArray.push(table.instance.tableGroup);
    this.tables.push(table);
  }

  onTableDelete(ref: ComponentRef<TableComponent>): void {
    this.tables = this.tables.filter(elem => elem != ref);
    this.tablesFormArray.removeAt(
      this.tablesFormArray.value.findIndex(group => group == ref.instance.tableGroup));
  }

  //to delete
  showTables() {
    console.log(this.tables);
    console.log(this.tablesFormArray);
    let finalList = [];
    this.tablesFormArray.controls.forEach(element => {
      finalList.push(this.tia.adapt((<FormGroup>element).getRawValue()));
    });
    console.log(finalList);
  }

}
