import { Injectable } from '@angular/core';
import { ValidatorFn, AbstractControl, FormArray } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class TableDataFormValidatorProviderService {

  constructor() { }

  getForTables(): Array<ValidatorFn> {
    let tableNamesAreDifferent: ValidatorFn = (tableArray: AbstractControl) => {
      let namesSet: Set<String> = new Set<String>();
      (<FormArray>tableArray).controls.forEach(element => {
        namesSet.add(element.value.name);
      });
      if ((<FormArray>tableArray).length != namesSet.size) {
        return { uniqueTableName: true }
      }
      return null;
    }
    return [tableNamesAreDifferent];
  }
}
