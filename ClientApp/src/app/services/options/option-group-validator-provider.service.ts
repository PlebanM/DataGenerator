import { Injectable } from '@angular/core';
import { Validators, ValidatorFn, AbstractControl, ValidationErrors } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class OptionGroupValidatorProviderService {

  validatorProviders: Map<string, Function>;

  constructor() {
    this.validatorProviders = new Map<string, Function>();
    this.validatorProviders.set("date", this.getForDate);
  }

  getValidatorsFor(columnType: string) {
    return this.validatorProviders.has(columnType) ? this.validatorProviders.get(columnType)() : [];
  }

  getForDate(): Array<ValidatorFn> {
    let fromDateNotGreaterThanToDateValidator = (control: AbstractControl): ValidationErrors => {
      let fromDate: Date = control.value.fromDate;
      let toDate: Date = control.value.toDate;
      if (fromDate != null && toDate != null) {
        if (fromDate > toDate) {
          return { fromDateGreaterThanToDate: true }
        }
      }
      return null;
    }
    let validators = [];
    validators.push(Validators.required, fromDateNotGreaterThanToDateValidator);
    return validators;
  }
}
