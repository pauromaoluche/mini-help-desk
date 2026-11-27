import { TicketPriority, TicketStatus } from '../response/ticket-summary-response';

export interface TicketFilter {
  status?: TicketStatus | null;
  priority?: TicketPriority | null;
}
