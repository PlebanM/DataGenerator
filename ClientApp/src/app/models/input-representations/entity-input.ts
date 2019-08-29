import { Injectable } from '@angular/core';
import { Adapter } from '../adapter';



export class EntityInput {
    constructor(private entName: string) { }
}

@Injectable({
    providedIn: "root"
})
export class EntityInputAdapter implements Adapter<EntityInput> {
    adapt(toAdapt: any): EntityInput {
        return new EntityInput(toAdapt.entName);
    }

}