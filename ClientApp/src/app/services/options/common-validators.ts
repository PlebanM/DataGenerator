import { ValidatorFn, Validators } from '@angular/forms';

export class CommonValidators {

    static getIsNumberValidator(): ValidatorFn {
        return Validators.pattern("\\d+");
    }
}