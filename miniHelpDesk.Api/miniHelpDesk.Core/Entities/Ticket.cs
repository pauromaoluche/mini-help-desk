using miniHelpDesk.Core.Entities.Enums;

namespace miniHelpDesk.Core.Entities
{
    public class Ticket : EntityBase
    {
        public string Title { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public TicketCategoryEnum Category { get; private set; }
        public TicketPriorityEnum Priority { get; private set; }
        public TicketStatusEnum Status { get; private set; } = TicketStatusEnum.Open;
        public int CreatedById { get; private set; }
        public User CreatedBy { get; private set; } = null!;
        public int? AssignedToId { get; private set; }
        public User? AssignedTo { get; private set; }

        private Ticket() { }

        public Ticket(
            string title,
            string description,
            TicketCategoryEnum category,
            TicketPriorityEnum priority,
            int createdById,
            int? assignedToId = null
            )
        {
            Title = title;
            Description = description;
            Category = category;
            Priority = priority;
            Status = TicketStatusEnum.Open;
            CreatedById = createdById;
            AssignedToId = assignedToId;
        }

        public bool UpdateStatus(TicketStatusEnum status)
        {
            if (Status == status)
                return false;

            Status = status;
            return true;
        }

        public bool AssignTo(int? assignedToId)
        {
            if (AssignedToId == assignedToId)
                return false;

            AssignedToId = assignedToId;
            return true;
        }
    }
}
