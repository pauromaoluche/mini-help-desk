import { Component } from '@angular/core';
import { TicketsFilter } from '../tickets-filter/tickets-filter';
import { Ticket, TicketItem } from '../ticket-item/ticket-item';

@Component({
  imports: [TicketsFilter, TicketItem],
  selector: 'app-tickets-table',
  templateUrl: './tickets-table.html',
})
export class TicketsTable {
  protected readonly tickets: Ticket[] = [];
}
