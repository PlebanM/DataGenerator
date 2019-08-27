import { TestBed } from '@angular/core/testing';

import { OptionGroupValidatorProviderService } from './option-group-validator-provider.service';

describe('OptionGroupValidatorProviderService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: OptionGroupValidatorProviderService = TestBed.get(OptionGroupValidatorProviderService);
    expect(service).toBeTruthy();
  });
});
