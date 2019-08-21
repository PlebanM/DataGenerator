import { SettingsInput, SettingsInputAdapter } from './settings-input';
import { RelationshipInput, RelationshipInputAdapter } from './relationships-input';
import { TableInput, TableInputAdapter } from './table-input';
import { Adapter } from '../adapter';
import { Injectable } from '@angular/core';

export class DataFormInput {
    constructor(private settings: SettingsInput, private relationships: Array<RelationshipInput>,
                private tables: Array<TableInput>) { }
}

@Injectable({
    providedIn: 'root'
})
export class DataFormInputAdapter implements Adapter<DataFormInput> {

    constructor(private settingsInputAdapter: SettingsInputAdapter,
                private relationshipAdapter: RelationshipInputAdapter,
                private tableInputAdapter: TableInputAdapter) { }

    adapt(toAdapt: any): DataFormInput {
        let settings = this.settingsInputAdapter.adapt(null); //has to change
        //let relationships = [this.relationshipAdapter.adapt(null)]; //has to change
        let relationships = []; //has to change
        let tables = [];
        toAdapt.tables.forEach(table => {
            tables.push(this.tableInputAdapter.adapt(table));
        });
        return new DataFormInput(settings, relationships, tables);
    }
}