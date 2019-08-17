import { Component, OnInit, Input, Output, EventEmitter, ComponentRef } from '@angular/core';
import { ColumnType } from '../models/column-type';
import { OptionTypeFinderService } from '../services/option-type-finder.service';

@Component({
  selector: 'app-column',
  templateUrl: './column.component.html',
  styleUrls: ['./column.component.css']
})
export class ColumnComponent implements OnInit {

  _ref: any;

  @Input()
  columnTypes: Array<ColumnType>;

  @Output()
  deleteEvent = new EventEmitter<ComponentRef<ColumnComponent>>();

  selectedType: string;

  constructor(private optionTypeFinder: OptionTypeFinderService) { }

  ngOnInit() {
  }

  getType(optionName: string): string {
    return this.optionTypeFinder.getType(optionName);
  }

  removeObject(): void {
    this.deleteEvent.emit(this._ref);
    this._ref.destroy();
  }

}
