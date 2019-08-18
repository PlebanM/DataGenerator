import { Injectable } from '@angular/core';
import { Adapter } from '../adapter';

//example class file, created as placeholder, can be changed or deleted

export class SettingsInput {
    constructor(private extractFileType: string) { }
}

@Injectable({
    providedIn: "root"
})
export class SettingsInputAdapter implements Adapter<SettingsInput> {

    constructor() { }

    adapt(columnToAdapt: any): SettingsInput {
        return new SettingsInput("csv");
    }

}