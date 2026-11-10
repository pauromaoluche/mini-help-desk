import { Component, Input } from '@angular/core';
import { TicketsFilter } from '../tickets-filter/tickets-filter';
import { TicketSummaryResponse } from '../../../models/response/ticket-summary-response';
import { TicketItem } from '../ticket-item/ticket-item';

@Component({
  imports: [TicketsFilter, TicketItem],
  selector: 'app-tickets-table',
  templateUrl: './tickets-table.html',
})
export class TicketsTable {
  @Input() tickets: TicketSummaryResponse[] = [];
}
