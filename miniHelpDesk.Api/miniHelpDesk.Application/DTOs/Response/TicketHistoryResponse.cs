using miniHelpDesk.Core.Entities.Enums;

namespace miniHelpDesk.Application.DTOs.Response
{
    public class TicketHistoryResponse
    {
        public int Id { get; set; }

        public int ChangedById { get; set; }
        public string ChangedByName { get; set; } = string.Empty;

        public int? OldAssignedToId { get; set; }
        public int? NewAssignedToId { get; set; }

        public string? OldAssignedToName { get; set; }
        public string? NewAssignedToName { get; set; }

        public TicketStatusEnum? OldStatus { get; set; }
        public TicketStatusEnum? NewStatus { get; set; }

        public string? Comment { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
