import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { tap } from 'rxjs';

import { API_BASE_URL } from '../config/api.config';
import { TicketSummaryResponse } from '../models/response/ticket-summary-response';

@Injectable({
  providedIn: 'root',
})
export class TicketService {
  private readonly http = inject(HttpClient);

  private readonly apiUrl = `${API_BASE_URL}/tickets`;

  getTickets() {
    return this.http.get<TicketSummaryResponse[]>(this.apiUrl).pipe(
      tap((tickets) => console.log('Fetched tickets:', tickets)),
    );
  }
}
