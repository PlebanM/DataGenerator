import { Component, OnInit, ComponentFactoryResolver, ViewChild, ViewContainerRef } from '@angular/core';
import { TableComponent } from '../table/table.component';

@Component({
  selector: 'app-data-form',
  templateUrl: './data-form.component.html',
  styleUrls: ['./data-form.component.css']
})
export class DataFormComponent implements OnInit {

  @ViewChild('tables', { static: true, read: ViewContainerRef })
  container: ViewContainerRef;

  constructor(private cfr: ComponentFactoryResolver) { }

  ngOnInit() {
  }

  addTable(): void {
    let tableFactory = this.cfr.resolveComponentFactory(TableComponent);
    let table = this.container.createComponent(tableFactory);
    table.instance._ref = table;
  }

}
