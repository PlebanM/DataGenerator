import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class OptionTypeFinderService {

  types: Map<string, string>;

  constructor() {
    let map = new Map<string, string>();
    map.set("from", "input");
    map.set("gap", "input");
    map.set("percentNull", "input");
    map.set("length", "input");
    map.set("unique", "checkbox");
    map.set("letters", "checkbox");
    map.set("numbers", "checkbox");
    map.set("whiteSigns", "input");
    map.set("fromDate", "date");
    map.set("toDate", "date");
    map.set("caseSensitive", "checkbox");
    this.types = map;
  }

  getType(optionName: string): string {
    return this.types.get(optionName);
  }


}
