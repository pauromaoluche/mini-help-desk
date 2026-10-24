using miniHelpDesk.Core.Entities.Enums;

namespace miniHelpDesk.Core.Entities
{
    public class User : EntityBase
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public UserRoleEnum Role { get; set; } = UserRoleEnum.User;
    }
}
