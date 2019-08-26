import { Injectable, OnInit } from '@angular/core';
import { ValidatorFn, Validators } from '@angular/forms';
import { CommonValidators } from './common-validators';

@Injectable({
  providedIn: 'root'
})
export class OptionValidatorProviderService {

  validatorProviders: Map<string, Function>

  constructor() {
    this.validatorProviders = new Map<string, Function>();
    this.validatorProviders.set("from", this.getForFrom);
    this.validatorProviders.set("gap", this.getForGap);
    this.validatorProviders.set("percentNull", this.getForPercentNull);
    this.validatorProviders.set("length", this.getForLength);
    this.validatorProviders.set("whiteSigns", this.getForWhiteSigns);
    this.validatorProviders.set("fromDate", this.getForFromDate);
    this.validatorProviders.set("toDate", this.getForToDate);
  }

  getValidatorsFor(optionName: string): Array<ValidatorFn> {
    return this.validatorProviders.has(optionName) ? this.validatorProviders.get(optionName)() : [];
  }

  getForFrom(): Array<ValidatorFn> {
    let validators = [];
    validators.push(Validators.required);
    validators.push(CommonValidators.getIsNumberValidator())
    return validators;
  }

  getForGap(): Array<ValidatorFn> {
    let validators = [];
    validators.push(Validators.required);
    validators.push(CommonValidators.getIsNumberValidator())
    return validators;
  }

  getForPercentNull(): Array<ValidatorFn> {
    let validators = [];
    validators.push(Validators.required);
    validators.push(Validators.min(0));
    validators.push(Validators.max(100));
    validators.push(CommonValidators.getIsNumberValidator())
    return validators;
  }

  getForLength(): Array<ValidatorFn> {
    let validators = [];
    validators.push(Validators.required);
    validators.push(Validators.min(1));
    validators.push(Validators.max(30));
    validators.push(CommonValidators.getIsNumberValidator())
    return validators;
  }

  getForWhiteSigns(): Array<ValidatorFn> {
    let validators = [];
    validators.push(Validators.required);
    validators.push(Validators.min(0));
    validators.push(CommonValidators.getIsNumberValidator())
    return validators;
  }

  getForFromDate(): Array<ValidatorFn> {
    let validators = [];
    validators.push(Validators.required);
    return validators;
  }

  getForToDate(): Array<ValidatorFn> {
    let validators = [];
    validators.push(Validators.required);
    return validators;
  }
}
