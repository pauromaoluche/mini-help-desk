import { Component, Input, Output, EventEmitter } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { TicketFilter } from '../../../models/response/ticket-filter';

@Component({
  selector: 'app-tickets-filter',
  imports: [FormsModule],
  templateUrl: './tickets-filter.html',
})
export class TicketsFilter {
  @Input() loading = false;
  @Output() search = new EventEmitter<TicketFilter>();

  filters: TicketFilter = {
    status: null,
    priority: null,
  };

  searchTickets(): void {
    this.search.emit({ ...this.filters });
  }
}
