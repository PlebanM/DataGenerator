import { ColumnInput, ColumnInputAdapter } from './column-input';
import { Injectable } from '@angular/core';
import { Adapter } from '../adapter';

export class TableInput {
    constructor(private name: string, private rowCount: string, private columnStructures: Array<ColumnInput>) { }
}

@Injectable({
    providedIn: 'root'
})

export class TableInputAdapter implements Adapter<TableInput> {

    constructor(private columnInputAdapter: ColumnInputAdapter) { }

    adapt(toAdapt: any): TableInput {
        const name = toAdapt.name;
        let rowCount = toAdapt.rowCount;
        let columnStructures = [];
        toAdapt.columnStructures.forEach(column => {
            columnStructures.push(this.columnInputAdapter.adapt(column));
        });
        return new TableInput(name, rowCount, columnStructures);
    }

}