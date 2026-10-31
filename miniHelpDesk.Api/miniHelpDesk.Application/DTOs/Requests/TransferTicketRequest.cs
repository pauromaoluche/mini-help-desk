using miniHelpDesk.Core.Entities.Enums;

namespace miniHelpDesk.Application.DTOs.Requests
{
    public class TransferTicketRequest
    {
        public int? NewAssignedToId { get; set; }
        public TicketCategoryEnum? Category { get; set; }

        public TicketPriorityEnum? Priority { get; set; }

        public int? SectorId { get; set; }
        public string? Comment { get; set; }
    }
}
