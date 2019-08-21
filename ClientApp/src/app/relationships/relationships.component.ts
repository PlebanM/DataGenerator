import { FormGroup, FormBuilder, FormArray } from '@angular/forms';
import { TableDataFormComponent } from './../table-data-form/table-data-form.component';
import { Component, OnInit, Input } from '@angular/core';


@Component({
  selector: 'app-relationships',
  templateUrl: './relationships.component.html',
  styleUrls: ['./relationships.component.css']
})
export class RelationshipsComponent implements OnInit {
  @Input() allTables: any[];
  @Input() dynamicdata: string;
  form: FormGroup;

  constructor(private fb: FormBuilder) { }

  ngOnInit() { 
    this.form = this.fb.group({
      items: this.fb.array([this.createItem()])
    })
  }

  createItem()  {
    return this.fb.group({
      name: ['John'],
      surname: ['doe'],
      entityOne: '',
      entityTwo: ''
    })
  }

  addNext(){
    (this.form.controls['items'] as FormArray).push(this.createItem())
  }

  submit(){
    console.log(this.form.value);
  }


}
