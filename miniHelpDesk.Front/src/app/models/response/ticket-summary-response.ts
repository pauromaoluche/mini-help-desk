export type TicketStatus = 0 | 1 | 2 | 3 | 4 | 5;
export type TicketPriority = 0 | 1 | 2 | 3;
export type TicketCategory = 0 | 1 | 2 | 3;

export interface TicketSummaryResponse {
  id: number;
  title: string;
  description: string;
  category: TicketCategory;
  status: TicketStatus;
  priority: TicketPriority;
  sectorId: number;
  assignedToId?: number | null;
  createdAt: string;
  updatedAt?: string | null;
}
