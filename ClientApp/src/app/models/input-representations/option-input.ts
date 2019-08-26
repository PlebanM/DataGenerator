import { Injectable } from '@angular/core';
import { Adapter } from '../adapter';
import { OptionTypeFinderService } from 'src/app/services/options/option-type-finder.service';
import { DatePipe } from '@angular/common';


export class OptionInput { }

@Injectable({
    providedIn: "root"
})
export class OptionInputAdapter implements Adapter<OptionInput> {

    constructor(private optionTypeFinder: OptionTypeFinderService, private datePipe: DatePipe) { }

    adapt(toAdapt: any): OptionInput {
        let names = Object.getOwnPropertyNames(toAdapt);
        let options = new OptionInput();

        names.forEach(name => {
            let currentType = this.optionTypeFinder.getType(name);
            if (currentType === "input") {
                options[name] = toAdapt[name];
            } else if (currentType === "checkbox") {
                if (toAdapt[name] === true) {
                    options[name] = '1';
                } else {
                    options[name] = '0';
                }
            } else if (currentType === "date") {
                options[name] = this.datePipe.transform(toAdapt[name], "yyyyMMdd");
            }
        });

        return options;
    }

}