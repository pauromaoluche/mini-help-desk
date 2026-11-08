import { Component, Input } from '@angular/core';

export interface Ticket {
  id: number;
  title: string;
  priority: string;
  status: string;
  assignee: string | null;
  progress: number;
}

@Component({
  selector: 'tr[app-ticket-item]',
  templateUrl: './ticket-item.html',
})
export class TicketItem {
  @Input({ required: true }) ticket!: Ticket;
}
