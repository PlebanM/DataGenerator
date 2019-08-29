import { TestBed } from '@angular/core/testing';

import { TableEntityService } from './table-entity.service';

describe('TableEntityService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: TableEntityService = TestBed.get(TableEntityService);
    expect(service).toBeTruthy();
  });
});
