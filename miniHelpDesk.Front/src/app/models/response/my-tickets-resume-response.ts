export interface MyTicketsResumeResponse {
  total: number;
  backlog: number;
  paused: number;
  inProgress: number;
  resolved: number;
  closed: number;
}
