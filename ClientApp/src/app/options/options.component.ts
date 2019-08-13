import { Component, OnInit } from '@angular/core';
import { OptionsService } from '../services/options.service';

@Component({
  selector: 'app-options',
  templateUrl: './options.component.html',
  styleUrls: ['./options.component.css']
})
export class OptionsComponent implements OnInit {

  constructor(private options: OptionsService) { }

  getOptions(): void {
    this.options.options().subscribe(res => {
      console.log(res);
    });
  }

  ngOnInit() {
  }



}
