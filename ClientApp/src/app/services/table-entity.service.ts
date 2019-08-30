import { TableComponent } from './../table/table.component';
import { TableDataFormComponent } from './../table-data-form/table-data-form.component';
import { Injectable } from '@angular/core';
import { FormArray } from '@angular/forms';


@Injectable({
  providedIn: 'root'
})
export class TableEntityService {
  tablesFormArray: FormArray;
  testLoop2: Array<string> = ['jedeYYYYYYYYn', 'dwa', 'trzy'];

  tableName: string;
  constructor() { }
  
  setTableFormFromArray(tablesFormArray: FormArray) {
   this.tablesFormArray = tablesFormArray;
  }

  getTableFormArray() {
    return this.tablesFormArray;
  }

}
