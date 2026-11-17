namespace miniHelpDesk.Application.DTOs.Response
{
    public class MyTicketResumeResponse
    {
        public int Total { get; set; } = 0;
        public int Backlog { get; set; } = 0;
        public int Paused { get; set; } = 0;
        public int InProgress { get; set; } = 0;
        public int Resolved { get; set; } = 0;
        public int Closed { get; set; } = 0;
    }
}
