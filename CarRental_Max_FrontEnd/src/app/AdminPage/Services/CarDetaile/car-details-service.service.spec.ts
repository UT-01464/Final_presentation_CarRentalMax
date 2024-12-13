import { TestBed } from '@angular/core/testing';

import { CarDetailsServiceService } from './car-details-service.service';

describe('CarDetailsServiceService', () => {
  let service: CarDetailsServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CarDetailsServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
