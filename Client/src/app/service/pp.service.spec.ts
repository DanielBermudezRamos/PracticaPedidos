import { TestBed } from '@angular/core/testing';

import { PpService } from './pp.service';

describe('PpService', () => {
  let service: PpService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PpService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
