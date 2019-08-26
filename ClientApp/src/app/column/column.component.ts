import { Component, OnInit, Input, Output, EventEmitter, ComponentRef } from '@angular/core';
import { ColumnType } from '../models/column-type';
import { OptionTypeFinderService } from '../services/options/option-type-finder.service';
import { FormGroup, FormControl } from '@angular/forms';
import { ColumnInputAdapter, ColumnInput } from '../models/input-representations/column-input';
import { OptionValidatorProviderService } from '../services/options/option-validator-provider.service';
import { OptionGroupValidatorProviderService } from '../services/options/option-group-validator-provider.service';

@Component({
  selector: 'app-column',
  templateUrl: './column.component.html',
  styleUrls: ['./column.component.css']
})
export class ColumnComponent implements OnInit {

  _ref: any;

  @Input()
  columnTypes: Array<ColumnType>;

  @Output()
  deleteEvent = new EventEmitter<ComponentRef<ColumnComponent>>();

  selectedType: ColumnType;

  columnGroup: FormGroup;

  constructor(private optionTypeFinder: OptionTypeFinderService,
    private columnInputAdapter: ColumnInputAdapter,
    private optionValidatorProvider: OptionValidatorProviderService,
    private optionGroupValidatorProvider: OptionGroupValidatorProviderService) {
    this.columnGroup = new FormGroup({
      name: new FormControl(),
      type: new FormControl(),
      options: new FormGroup({})
    })
  }

  ngOnInit() {
  }

  getType(optionName: string): string {
    return this.optionTypeFinder.getType(optionName);
  }

  removeObject(): void {
    this.deleteEvent.emit(this._ref);
    this._ref.destroy();
  }

  //to delete
  showGroup() {
    console.log(this.columnGroup);
    console.log(this.columnGroup.getRawValue());
    this.getInputRepresentation();
  }

  onSelectedTypeChange() {
    this.repopulateOptions();
    this.selectedType = this.columnGroup.get("type").value;
  }

  private repopulateOptions() {
    this.columnGroup.removeControl("options");
    let columnType = this.columnGroup.get("type");
    let options = new FormGroup({}, this.optionGroupValidatorProvider.getValidatorsFor(columnType.value.type.name));
    columnType.value.options.forEach(element => {
      options.addControl(element.name,
        new FormControl(null, this.optionValidatorProvider.getValidatorsFor(element.name)));
    });
    this.columnGroup.addControl("options", options);
  }

  //to delete
  getInputRepresentation(): ColumnInput {
    // console.log(this.columnInputAdapter.adapt(this.columnGroup.getRawValue()));
    // console.log(JSON.stringify(this.columnInputAdapter.adapt(this.columnGroup.getRawValue())));
    return this.columnInputAdapter.adapt(this.columnGroup.getRawValue());
  }

}
