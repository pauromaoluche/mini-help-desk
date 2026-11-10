import { provideHttpClient } from '@angular/common/http';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { API_BASE_URL } from '../config/api.config';
import { TicketSummaryResponse } from '../models/response/ticket-summary-response';
import { TicketService } from './ticket-service';

describe('TicketService', () => {
  let service: TicketService;
  let httpTesting: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [provideHttpClient(), provideHttpClientTesting()],
    });
    service = TestBed.inject(TicketService);
    httpTesting = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpTesting.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('returns the ticket summaries from the API', () => {
    const tickets: TicketSummaryResponse[] = [{
      id: 1,
      title: 'Printer',
      description: 'Not printing',
      category: 0,
      status: 0,
      priority: 2,
      sectorId: 1,
      createdAt: '2026-01-01T00:00:00Z',
    }];
    let result: TicketSummaryResponse[] | undefined;

    service.getTickets().subscribe((response) => {
      result = response;
    });

    const request = httpTesting.expectOne(`${API_BASE_URL}/tickets`);
    expect(request.request.method).toBe('GET');
    request.flush(tickets);
    expect(result).toEqual(tickets);
  });
});
