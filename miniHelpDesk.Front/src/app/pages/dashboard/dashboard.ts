import { Component } from '@angular/core';
import { TicketService } from '../../services/ticket-service';
import { Header } from '../../components/header/header';
import { Container } from '../../components/container/container';
import { TicketsTable } from '../../components/tickets/tickets-table/tickets-table';
import { TicketSummaryResponse } from '../../models/response/ticket-summary-response';

@Component({
  selector: 'app-dashboard',
  imports: [Header, Container, TicketsTable],
  templateUrl: './dashboard.html',
})
export class Dashboard {
  tickets: TicketSummaryResponse[] = [];

  constructor(private ticketService: TicketService) {}

  ngOnInit(): void {
    this.ticketService.getTickets().subscribe({
      next: (tickets) => {
        console.log('Chamados recebidos:', tickets);
        this.tickets = tickets;
      },
      error: (error) => {
        console.error('Erro ao buscar chamados:', error);
      },
    });
  }
}
