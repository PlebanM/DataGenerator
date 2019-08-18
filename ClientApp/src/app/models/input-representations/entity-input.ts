import { Injectable } from '@angular/core';
import { Adapter } from '../adapter';

//example class file, created as placeholder, can be changed or deleted

export class EntityInput {
    constructor(private tableName: string, private columnName: string, private modality: string,
        private cardinality: string) { }
}

@Injectable({
    providedIn: "root"
})
export class EntityInputAdapter implements Adapter<EntityInput> {
    adapt(ex: any): EntityInput {
        return new EntityInput(ex.tableName, ex.columnName, ex.modality, ex.cardinality);
    }

}