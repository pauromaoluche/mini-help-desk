import { Component, Input } from '@angular/core';
import { NgClass } from '@angular/common';
import { TicketSummaryResponse } from '../../../models/response/ticket-summary-response';

@Component({
  imports: [NgClass],
  selector: 'tr[app-ticket-item]',
  templateUrl: './ticket-item.html',
})
export class TicketItem {
  @Input({ required: true }) ticket!: TicketSummaryResponse;

  protected readonly priorities = ['Low', 'Medium', 'High', 'Critical'];
  protected readonly statuses = ['Backlog', 'UserBacklog', 'Paused', 'InProgress', 'Resolved', 'Closed'];
}
