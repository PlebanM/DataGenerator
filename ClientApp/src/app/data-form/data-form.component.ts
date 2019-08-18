import { Component, OnInit, ComponentFactoryResolver, ViewChild, ViewContainerRef, ComponentRef, AfterViewInit } from '@angular/core';
import { TableComponent } from '../table/table.component';
import { FormGroup, FormControl, FormArray } from '@angular/forms';
import { TableDataFormComponent } from '../table-data-form/table-data-form.component';
import { DataFormInputAdapter } from '../models/input-representations/data-form-input';
import { InputDataSenderService } from '../services/input-data-sender.service';

@Component({
  selector: 'app-data-form',
  templateUrl: './data-form.component.html',
  styleUrls: ['./data-form.component.css']
})
export class DataFormComponent implements OnInit, AfterViewInit {

  @ViewChild('tableform', { static: false }) tables: TableDataFormComponent

  formData: FormGroup;

  constructor(private dataFormInputAdapter: DataFormInputAdapter, private sender: InputDataSenderService) { }

  ngOnInit(): void {
  }

  ngAfterViewInit(): void {

    this.formData = new FormGroup({
      settings: new FormControl("temporary"),
      relationships: new FormArray([]), //temporary
      tables: this.tables.tablesFormArray
    })
  }

  sendForm(): void {
    // console.log(this.formData.getRawValue());
    // console.log(this.dataFormInputAdapter.adapt(this.formData.getRawValue()));
    // console.log(JSON.stringify(this.dataFormInputAdapter.adapt(this.formData.getRawValue())));
    this.sender.send(this.dataFormInputAdapter.adapt(this.formData.getRawValue()));
  }
}
