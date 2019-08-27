import { TestBed } from '@angular/core/testing';

import { OptionValidatorProviderService } from './option-validator-provider.service';

describe('OptionValidatorProviderService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: OptionValidatorProviderService = TestBed.get(OptionValidatorProviderService);
    expect(service).toBeTruthy();
  });
});
