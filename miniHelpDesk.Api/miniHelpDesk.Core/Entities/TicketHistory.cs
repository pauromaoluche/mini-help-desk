using miniHelpDesk.Core.Entities.Enums;

namespace miniHelpDesk.Core.Entities
{
    public class TicketHistory : EntityBase
    {
        public int TicketId { get; private set; }
        public Ticket Ticket { get; private set; } = null!;

        public int ChangedById { get; private set; }
        public User ChangedBy { get; private set; } = null!;

        public int? OldAssignedToId { get; private set; }
        public int? NewAssignedToId { get; private set; }

        public TicketStatusEnum? OldStatus { get; private set; }
        public TicketStatusEnum? NewStatus { get; private set; }

        public string? Comment { get; private set; }

        private TicketHistory() { }

        public TicketHistory(
            int ticketId,
            int changedById,
            int? oldAssignedToId,
            int? newAssignedToId,
            TicketStatusEnum? oldStatus,
            TicketStatusEnum? newStatus,
            string? comment)
        {
            TicketId = ticketId;
            ChangedById = changedById;
            OldAssignedToId = oldAssignedToId;
            NewAssignedToId = newAssignedToId;
            OldStatus = oldStatus;
            NewStatus = newStatus;
            Comment = comment;
        }
    }
}
