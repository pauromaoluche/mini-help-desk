using miniHelpDesk.Core.Entities.Enums;

namespace miniHelpDesk.Core.Entities
{
    public class Ticket : EntityBase
    {
        public string Title { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public TicketCategoryEnum Category { get; private set; }
        public TicketPriorityEnum Priority { get; private set; }
        public TicketStatusEnum Status { get; private set; } = TicketStatusEnum.Backlog;
        public int CreatedById { get; private set; }
        public User CreatedBy { get; private set; } = null!;
        public int? AssignedToId { get; private set; }
        public User? AssignedTo { get; private set; }
        public int SectorId { get; private set; }
        public Sector Sector { get; private set; } = null!;
        public List<TicketHistory> History { get; private set; } = [];

        private Ticket() { }

        public Ticket(
            string title,
            string description,
            TicketCategoryEnum category,
            TicketPriorityEnum priority,
            int createdById,
            int sectorId,
            int? assignedToId = null
            )
        {
            Title = title;
            Description = description;
            Category = category;
            Priority = priority;
            Status = TicketStatusEnum.Backlog;
            CreatedById = createdById;
            SectorId = sectorId;
            AssignedToId = assignedToId;
        }

        public bool UpdateStatus(TicketStatusEnum? status)
        {
            if (!status.HasValue || Status == status.Value)
                return false;

            Status = status.Value;
            return true;
        }

        public bool UpdateCategory(TicketCategoryEnum? category)
        {
            if (!category.HasValue || Category == category.Value)
                return false;
            Category = category.Value;
            return true;
        }

        public bool AssignTo(int? assignedToId)
        {
            if (AssignedToId == assignedToId)
                return false;

            AssignedToId = assignedToId;
            return true;
        }

        public bool UpdatePriority(TicketPriorityEnum? priority)
        {
            if (!priority.HasValue || Priority == priority.Value)
                return false;
            Priority = priority.Value;
            return true;
        }

        public bool UpdateSector(int? sectorId)
        {
            if (!sectorId.HasValue || SectorId == sectorId.Value)
                return false;

            SectorId = sectorId.Value;
            return true;
        }

        public bool UpdateProgress(
            TicketStatusEnum? status = null,
            TicketCategoryEnum? category = null,
            TicketPriorityEnum? priority = null,
            int? sectorId = null,
            int? assignedToId = null)
        {
            var finalStatus = status ?? Status;
            var finalCategory = category ?? Category;
            var finalPriority = priority ?? Priority;
            var finalSectorId = sectorId ?? SectorId;

            var statusUpdated = UpdateStatus(finalStatus);
            var categoryUpdated = UpdateCategory(finalCategory);
            var priorityUpdated = UpdatePriority(finalPriority);
            var sectorUpdated = UpdateSector(finalSectorId);
            var assignedToUpdated = AssignTo(assignedToId);

            return statusUpdated || categoryUpdated || priorityUpdated || sectorUpdated || assignedToUpdated;
        }
    }
}
