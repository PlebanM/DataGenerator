import { TestBed } from '@angular/core/testing';

import { InputDataSenderService } from './input-data-sender.service';

describe('InputDataSenderService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: InputDataSenderService = TestBed.get(InputDataSenderService);
    expect(service).toBeTruthy();
  });
});
