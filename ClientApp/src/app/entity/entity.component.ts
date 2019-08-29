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
  testLoop: Array<string> = ['jeden', 'dwa', 'trzy'];
  modalityOne: any = ['one', 'zero'];
  cardinalityOne: any = ['many', 'one'];
  modalityTwo: any = ['one', 'zero'];
  cardinalityTwo: any = ['many'];
  testLoop2: any[];

  testInputTable: string;

  entGroup: FormGroup;

  entityGroup: FormGroup = new FormGroup({
    entName: new FormControl(null)
  });

  tablesFormArray: FormArray;
  tableRaw: any[] = [];


  constructor(private serviceEnt: TableEntityService, private tia: TableInputAdapter,) { }

  ngOnInit() {
  this.testLoop2 = this.serviceEnt.testLoop2;
  this.tablesFormArray = this.serviceEnt.tablesFormArray;
  
  this.tablesFormArray.controls.forEach(element => {
    this.tableRaw.push(this.tia.adapt((<FormGroup>element).getRawValue()));
  });

  }

}
