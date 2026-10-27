using miniHelpDesk.Core.Entities.Enums;

namespace miniHelpDesk.Application.DTOs.Response
{
    public class TicketDetailResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TicketCategoryEnum Category { get; set; }
        public TicketPriorityEnum Priority { get; set; }
        public TicketStatusEnum Status { get; set; }
        public int CreatedById { get; set; }
        public int? AssignedToId { get; set; }
        public string? AssignedToName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public List<TicketHistoryResponse> History { get; set; } = [];

    }
}
