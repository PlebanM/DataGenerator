import { TestBed } from '@angular/core/testing';

import { ColumnTypesGetterService } from './column-types-getter.service';

describe('ColumnTypesGetterService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ColumnTypesGetterService = TestBed.get(ColumnTypesGetterService);
    expect(service).toBeTruthy();
  });
});
