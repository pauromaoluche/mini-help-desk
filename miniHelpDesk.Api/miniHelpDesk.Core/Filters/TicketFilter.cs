using miniHelpDesk.Core.Entities.Enums;

namespace miniHelpDesk.Core.Filters
{
    public class TicketFilter
    {
        public TicketStatusEnum? Status { get; set; }

        public TicketCategoryEnum? Category { get; set; }

        public TicketPriorityEnum? Priority { get; set; }

        public int? AssignedToId { get; set; }
    }
}
