import { Component, signal } from '@angular/core';
import { finalize } from 'rxjs';
import { TicketFilter } from '../../models/response/ticket-filter';
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

  readonly loadingTickets = signal(false);

  constructor(private ticketService: TicketService) {}

  ngOnInit(): void {
    this.searchTickets();

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

  searchTickets(filters: TicketFilter = {}): void {
    if (this.loadingTickets()) return;

    this.loadingTickets.set(true);

    this.ticketService
      .getTickets(filters)
      .pipe(finalize(() => this.loadingTickets.set(false)))
      .subscribe({
        next: (tickets) => this.tickets.set(tickets),
        error: (error) => console.error('Erro ao buscar chamados:', error),
      });
  }
}
