import { Adapter } from './adapter';
import { Injectable } from '@angular/core';

export class TypeInfo {
    constructor(private name: string, private description: string) { };
}

@Injectable({
    providedIn: 'root'
})
export class TypeInfoAdapter implements Adapter<TypeInfo> {

    adapt(type: any): TypeInfo {
        return new TypeInfo(type.type, type.description);
    }

}