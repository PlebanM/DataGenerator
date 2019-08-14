import { Adapter } from './adapter';
import { Injectable } from '@angular/core';

export class Option {
    constructor(private name: string, private description: string) { };
}

@Injectable({
    providedIn: 'root'
})
export class OptionAdapter implements Adapter<Option> {

    adapt(option: any): Option {
        return new Option(option.name, option.description);
    }

}