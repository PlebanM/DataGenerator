import { Component, OnInit, Input } from '@angular/core';
import { ColumnType } from '../models/column-type';

@Component({
  selector: 'app-column',
  templateUrl: './column.component.html',
  styleUrls: ['./column.component.css']
})
export class ColumnComponent implements OnInit {

  @Input()
  columnTypes: Array<ColumnType>;

  constructor() { }

  ngOnInit() {
  }

}
