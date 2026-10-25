using miniHelpDesk.Core.Entities.Enums;

namespace miniHelpDesk.Application.DTOs.Response
{
    public class TicketSummaryResponse
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TicketStatusEnum Status { get; set; }

    }
}
