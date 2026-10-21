using System;
using System.Collections.Generic;
using System.Text;

namespace miniHelpDesk.Core.Entities
{
    public class User
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string Role { get; set; }
    }
}
