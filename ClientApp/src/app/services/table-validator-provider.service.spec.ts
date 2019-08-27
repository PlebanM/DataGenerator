import { TestBed } from '@angular/core/testing';

import { TableValidatorProviderService } from './table-validator-provider.service';

describe('TableValidatorProviderService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: TableValidatorProviderService = TestBed.get(TableValidatorProviderService);
    expect(service).toBeTruthy();
  });
});
