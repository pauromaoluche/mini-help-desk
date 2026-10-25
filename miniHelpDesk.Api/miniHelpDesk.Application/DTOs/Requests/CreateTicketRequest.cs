using miniHelpDesk.Core.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace miniHelpDesk.Application.DTOs.Requests
{
    public class CreateTicketRequest
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MinLength(3)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public TicketCategoryEnum Category { get; set; }

        [Required]
        public TicketPriorityEnum Priority { get; set; }

        public int AssignedToId { get; set; } = 0;

    }
}
