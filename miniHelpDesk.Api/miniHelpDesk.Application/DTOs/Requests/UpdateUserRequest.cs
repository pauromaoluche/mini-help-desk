using miniHelpDesk.Core.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace miniHelpDesk.Application.DTOs.Requests
{
    public class UpdateUserRequest
    {
        [StringLength(100, MinimumLength = 3)]
        public string? Name { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [MinLength(8)]
        public string? Password { get; set; }

        public UserRoleEnum? Role { get; set; }
    }
}
