import { OptionInputAdapter, OptionInput } from './option-input';
import { Injectable } from '@angular/core';
import { Adapter } from '../adapter';

export class ColumnInput {
    constructor(private name: string,
        private type: string,
        private options: OptionInput) { }
}

@Injectable({
    providedIn: "root"
})
export class ColumnInputAdapter implements Adapter<ColumnInput> {

    constructor(private optionAdapter: OptionInputAdapter) { }

    adapt(columnToAdapt: any): ColumnInput {
        let name = columnToAdapt.name;
        let type = columnToAdapt.type.type.name;
        let options = this.optionAdapter.adapt(columnToAdapt.options);
        return new ColumnInput(name, type, options);
    }

}