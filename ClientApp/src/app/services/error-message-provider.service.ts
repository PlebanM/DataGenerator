import { Injectable } from '@angular/core';
import { ValidationErrors } from '@angular/forms';
import { stringify } from 'querystring';

@Injectable({
  providedIn: 'root'
})
export class ErrorMessageProviderService {

  private errorMessages: Map<string, string>;
  private errorTemplateMap: Map<string, Function>

  constructor() {

    let fillTemplate = function (templateString, variables) {
      return new Function("return `" + templateString + "`;").call(variables);
    }

    this.errorTemplateMap = new Map<string, Function>();
    this.errorTemplateMap.set("min", fillTemplate);

    this.errorMessages = new Map<string, string>();
    this.errorMessages.set("required", "This field is required.");
    this.errorMessages.set("notANumber", "Provide a number");
    this.errorMessages.set("min", "Number must be greater than ${this.min.min}");
    this.errorMessages.set("uniqueColumnName", "Column names must be unique");
    this.errorMessages.set("uniqueTableName", "Table names must be unique");
    this.errorMessages.set("fromDateGreaterThanToDate", "'From Date' cannot be greater than 'To Date'");
  }

  getMessageForRandomError(errors: ValidationErrors): string {
    if (errors == null) {
      return "";
    }
    let propOfErr = Object.getOwnPropertyNames(errors);
    if (propOfErr.length > 0) {
      let err = this.errorMessages.get(propOfErr[0]);
      let fillTemplate = this.errorTemplateMap.get(propOfErr[0]);
      if (err && fillTemplate) {
        return fillTemplate(err, errors);
      }
      return err ? err : "";
    }
    return "";
  }


}
