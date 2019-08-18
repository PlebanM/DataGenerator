import { Injectable } from '@angular/core';
import { Adapter } from '../adapter';
import { EntityInput, EntityInputAdapter } from './entity-input';

//example class file, created as placeholder, can be changed or deleted

export class RelationshipInput {
    constructor(private entityOne: EntityInput, private entityTwo: EntityInput) { }
}

@Injectable({
    providedIn: "root"
})
export class RelationshipInputAdapter implements Adapter<RelationshipInput> {

    constructor(private entityInputAdapter: EntityInputAdapter) { }

    adapt(columnToAdapt: any): RelationshipInput {
        let one = {
            tableName: "myTable",
            columnName: "City",
            modality: "one",
            cardinality: "many"
        }
        let two = {
            tableName: "myTable2",
            columnName: "ID",
            modality: "one",
            cardinality: "one"
        }
        return new RelationshipInput(this.entityInputAdapter.adapt(one), this.entityInputAdapter.adapt(two));
    }

}