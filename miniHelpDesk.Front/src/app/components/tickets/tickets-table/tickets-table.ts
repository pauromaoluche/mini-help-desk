import { Component, Input, Output, EventEmitter } from '@angular/core';
import { TicketsFilter } from '../tickets-filter/tickets-filter';
import { TicketSummaryResponse } from '../../../models/response/ticket-summary-response';
import { TicketItem } from '../ticket-item/ticket-item';
import { TicketFilter } from '../../../models/response/ticket-filter';

@Component({
  imports: [TicketsFilter, TicketItem],
  selector: 'app-tickets-table',
  templateUrl: './tickets-table.html',
})
export class TicketsTable {
  @Input() tickets: TicketSummaryResponse[] = [];
  @Input() loading = false;
  @Output() search = new EventEmitter<TicketFilter>();
}
