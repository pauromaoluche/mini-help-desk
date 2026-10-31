using System.ComponentModel.DataAnnotations;

namespace miniHelpDesk.Core.Entities
{
    public class Sector : EntityBase
    {
        public string Name { get; private set; } = string.Empty;

        public ICollection<User> Users { get; set; } = new List<User>();

        private Sector() { }

        public Sector(string name)
        {
            Name = name;
        }
    }
}
