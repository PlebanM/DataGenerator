import { TestBed } from '@angular/core/testing';

import { ErrorMessageProviderService } from './error-message-provider.service';

describe('ErrorMessageProviderService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ErrorMessageProviderService = TestBed.get(ErrorMessageProviderService);
    expect(service).toBeTruthy();
  });
});
