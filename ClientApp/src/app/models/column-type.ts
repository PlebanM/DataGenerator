import { Adapter } from './adapter';
import { TypeInfo, TypeInfoAdapter } from './type-info';
import { Option, OptionAdapter } from './option';
import { Injectable } from '@angular/core';

export class ColumnType {
    constructor(private type: TypeInfo, private options: Array<Option>) { }
}

@Injectable({
    providedIn: 'root'
})
export class ColumnTypeAdapter implements Adapter<ColumnType> {

    constructor(private typeInfoAdapter: TypeInfoAdapter, private optionAdapter: OptionAdapter) { }

    adapt(columnType: any): ColumnType {
        let typeInfo = this.typeInfoAdapter.adapt(columnType.type);
        let options = [];
        columnType.options.forEach(option => {
            options.push(this.optionAdapter.adapt(option));
        });
        return new ColumnType(typeInfo, options);
    }

}