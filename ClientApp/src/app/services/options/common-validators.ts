import { ValidatorFn, Validators, AbstractControl } from '@angular/forms';
import { stringify } from 'querystring';

export class CommonValidators {

    static getIsNumberValidator(): ValidatorFn {
        return Validators.pattern("\\d+");
    }

    static isNumber(control: AbstractControl) {
        let regex = new RegExp('^-?\\d*$');
        return regex.test(control.value) ? null : { notANumber: true };
    }
}