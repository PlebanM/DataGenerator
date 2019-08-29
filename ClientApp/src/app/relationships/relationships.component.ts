import { TableEntityService } from './../services/table-entity.service';
import { EntityInputAdapter } from './../models/input-representations/entity-input';
import { EntityComponent } from './../entity/entity.component';
import { FormGroup, FormBuilder, FormArray } from '@angular/forms';
import { Component, OnInit, ViewChild, ViewContainerRef, ComponentRef, ComponentFactoryResolver } from '@angular/core';

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

  constructor(private fb: FormBuilder, private cfr: ComponentFactoryResolver,
    private eia: EntityInputAdapter, private tableEntityService: TableEntityService ) {
      this.entitiesFormArray = new FormArray([]);
  }

  ngOnInit() {
  }

  addRelation() {
    let entityFactory = this.cfr.resolveComponentFactory(EntityComponent);
    let entity = this.container.createComponent(entityFactory);
    entity.instance._ref = entity;
    this.entitiesFormArray.push(entity.instance.entityGroup);
    this.entities.push(entity);
      
  }



}
