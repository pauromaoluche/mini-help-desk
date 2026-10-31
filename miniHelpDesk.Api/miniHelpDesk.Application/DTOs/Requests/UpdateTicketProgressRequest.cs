using miniHelpDesk.Core.Entities.Enums;

namespace miniHelpDesk.Application.DTOs.Requests
{
    public class UpdateTicketProgressRequest
    {
        public TicketStatusEnum Status { get; set; }

        public TicketCategoryEnum Category { get; set; }

        public TicketPriorityEnum Priority { get; set; }

        public string? Comment { get; set; }
    }
}
