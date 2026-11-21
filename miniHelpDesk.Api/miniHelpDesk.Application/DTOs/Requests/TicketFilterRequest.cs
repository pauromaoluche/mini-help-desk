using miniHelpDesk.Core.Entities.Enums;

namespace miniHelpDesk.Application.DTOs.Requests
{
    public class TicketFilterRequest
    {
        public TicketStatusEnum? Status { get; set; }

        public TicketCategoryEnum? Category { get; set; }

        public TicketPriorityEnum? Priority { get; set; }

        public int? AssignedToId { get; set; }
    }
}
