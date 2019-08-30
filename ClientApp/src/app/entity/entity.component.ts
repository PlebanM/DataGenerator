import { createWiresService } from 'selenium-webdriver/firefox';
import { Observable } from 'rxjs';
import { Component, OnInit, Input } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, FormArray } from '@angular/forms';
import { TableEntityService } from '../services/table-entity.service';
import { TableInputAdapter } from '../models/input-representations/table-input';

@Component({
  selector: 'app-entity',
  templateUrl: './entity.component.html',
  styleUrls: ['./entity.component.css']
})
export class EntityComponent implements OnInit {

  _ref: any;

  modalityOne: any = ['one', 'zero'];
  cardinalityOne: any = ['many', 'one'];
  modalityTwo: any = ['one', 'zero'];
  cardinalityTwo: any = ['many'];
  tableRaw: any[] = [];
  tablesFormArray: FormArray;


  relationships: FormGroup = new FormGroup({
    entityOne: new FormGroup({
      tableName: new FormControl(null),
      columnName: new FormControl(null),
      modality: new FormControl(null),
      cardinality: new FormControl(null)
    }),
    entityTwo: new FormGroup({
      tableName: new FormControl(null),
      columnName: new FormControl(null),
      modality: new FormControl(null),
      cardinality: new FormControl(null)
    })

  });



  constructor(private serviceEnt: TableEntityService, private tia: TableInputAdapter) { }

  ngOnInit() {
  this.tablesFormArray = this.serviceEnt.tablesFormArray;

  this.tablesFormArray.controls.forEach(element => {
    this.tableRaw.push(this.tia.adapt((<FormGroup>element).getRawValue()));
  });
  console.log(this.tableRaw);
  console.log(this.tablesFormArray);
  }

  
  getTableByName(name: string) {

    for (let index = 0; index < this.tableRaw.length; index++) {
      const element = this.tableRaw[index];
      if(element.name === name)
      {console.log(element);
        return element; }
    }
    return [];
  }


}
