import { Component, signal } from '@angular/core';
import { Resume } from '../../components/resume/resume';
import { TicketService } from '../../services/ticket-service';
import { Header } from '../../components/header/header';
import { Container } from '../../components/container/container';
import { TicketsTable } from '../../components/tickets/tickets-table/tickets-table';
import { TicketSummaryResponse } from '../../models/response/ticket-summary-response';
import { MyTicketsResumeResponse } from '../../models/response/my-tickets-resume-response';

@Component({
  selector: 'app-dashboard',
  imports: [Header, Container, TicketsTable, Resume],
  templateUrl: './dashboard.html',
})
export class Dashboard {
  readonly tickets = signal<TicketSummaryResponse[]>([]);

  readonly resume = signal<MyTicketsResumeResponse | null>(null);

  constructor(private ticketService: TicketService) {}

  ngOnInit(): void {
    this.ticketService.getTickets().subscribe({
      next: (tickets) => {
        console.log('Chamados recebidos:', tickets);
        this.tickets.set(tickets);
      },
      error: (error) => {
        console.error('Erro ao buscar chamados:', error);
      },
    });
    this.ticketService.getMyTicketResume().subscribe({
      next: (resume) => {
        console.log('Resumo de chamados recebidos:', resume);
        this.resume.set(resume);
      },
      error: (error) => {
        console.error('Erro ao buscar resumo de chamados:', error);
      },
    });
  }
}
