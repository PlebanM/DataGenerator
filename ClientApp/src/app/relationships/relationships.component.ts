import { TableEntityService } from './../services/table-entity.service';
import { EntityInputAdapter } from './../models/input-representations/entity-input';
import { EntityComponent } from './../entity/entity.component';
import { FormGroup, FormBuilder, FormArray } from '@angular/forms';
import { Component, OnInit, ViewChild, ViewContainerRef, ComponentRef, ComponentFactoryResolver } from '@angular/core';
import { createWiresService } from 'selenium-webdriver/firefox';

@Component({
  selector: 'app-relationships',
  templateUrl: './relationships.component.html',
  styleUrls: ['./relationships.component.css']
})
export class RelationshipsComponent implements OnInit {
  
  @ViewChild('entity', { static: true, read: ViewContainerRef })
  container: ViewContainerRef;

  entities: Array<ComponentRef<EntityComponent>> = [];
  entitiesFormArray: FormArray;
  
  tableRead: any;
  tableRaw: any[] = [];

  constructor( private fb: FormBuilder, private cfr: ComponentFactoryResolver,
    private eia: EntityInputAdapter, private tableEntityService: TableEntityService ) {
      this.entitiesFormArray = new FormArray([]);
  }

  ngOnInit() {
    
  }

  addRelation() {
    let entityFactory = this.cfr.resolveComponentFactory(EntityComponent);
    let entity = this.container.createComponent(entityFactory);
    entity.instance._ref = entity;
    this.entitiesFormArray.push(entity.instance.relationships);
    this.entities.push(entity);
    
    console.log(this.entitiesFormArray);
    
      
  }

  changeTableNameFromIndexToName(tableFormArray){

    for (let index = 0; index < this.entitiesFormArray.value.length; index++) {
      this.entitiesFormArray.value[index].entityOne.tableName = tableFormArray.value[this.entitiesFormArray.value[index].entityOne.tableName].name;
      this.entitiesFormArray.value[index].entityTwo.tableName = tableFormArray.value[this.entitiesFormArray.value[index].entityTwo.tableName].name;
    }}

}
