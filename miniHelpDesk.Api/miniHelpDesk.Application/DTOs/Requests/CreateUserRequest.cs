using miniHelpDesk.Core.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace miniHelpDesk.Application.DTOs.Requests
{
    public class CreateUserRequest
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(8)]
        public string Password { get; set; } = string.Empty;

        [Required]
        public UserRoleEnum Role { get; set; } = UserRoleEnum.User;
    }
}
