export type TicketStatus = 'Open' | 'InProgress' | 'Resolved' | 'Closed';
export type TicketPriority = 'Low' | 'Medium' | 'High' | 'Critical';

export interface TicketResponse {
  id: number;
  title: string;
  description: string;
  category: string;
  status: TicketStatus;
  priority: TicketPriority;
  requesterId: number;
  assigneeId?: number | null;
  createdAt: string;
  updatedAt?: string;
}
