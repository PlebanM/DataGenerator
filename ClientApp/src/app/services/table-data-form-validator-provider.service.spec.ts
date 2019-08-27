import { TestBed } from '@angular/core/testing';

import { TableDataFormValidatorProviderService } from './table-data-form-validator-provider.service';

describe('TableDataFormValidatorProviderService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: TableDataFormValidatorProviderService = TestBed.get(TableDataFormValidatorProviderService);
    expect(service).toBeTruthy();
  });
});
