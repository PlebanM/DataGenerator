import { Injectable } from '@angular/core';
import { ValidatorFn, Validators, AbstractControl, FormArray } from '@angular/forms';
import { CommonValidators } from './options/common-validators';

@Injectable({
  providedIn: 'root'
})
export class TableValidatorProviderService {

  constructor() { }

  getForRowCount(): Array<ValidatorFn> {
    let validators = [];
    validators.push(Validators.required, CommonValidators.getIsNumberValidator(), Validators.min(1));
    return validators;
  }

  getForColumnStructures(): Array<ValidatorFn> {
    let columnNamesAreDifferent: ValidatorFn = (columnArray: AbstractControl) => {
      let namesSet: Set<String> = new Set<String>();
      (<FormArray>columnArray).controls.forEach(element => {
        namesSet.add(element.value.name);
      });
      if ((<FormArray>columnArray).length != namesSet.size) {
        return { uniqueColumnName: true }
      }
      return null;
    }
    return [columnNamesAreDifferent];
  }
}
