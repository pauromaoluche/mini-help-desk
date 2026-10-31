using miniHelpDesk.Core.Entities.Enums;

namespace miniHelpDesk.Core.Entities
{
    public class User : EntityBase
    {
        private readonly List<Sector> _sectors = new();

        public string Name { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string Password { get; private set; } = string.Empty;
        public UserRoleEnum Role { get; private set; } = UserRoleEnum.User;

        public ICollection<Sector> Sectors { get; set; } = new List<Sector>();

        private User() { }

        public User(string name, string email, string password, UserRoleEnum role, IEnumerable<Sector>? sectors = null)
        {
            Name = name;
            Email = email;
            Password = password;
            Role = role;

            if (sectors != null)
            {
                _sectors.AddRange(sectors);
            }
        }

        public void UpdateDetails(string name, string email, UserRoleEnum role)
        {
            Name = name;
            Email = email;
            Role = role;
        }

        public void UpdatePassword(string password)
        {
            if (!string.IsNullOrEmpty(password))
            {
                Password = password;
            }
        }

        public void UpdateSectors(IEnumerable<Sector> newSectors)
        {
            var sectorsToRemove = _sectors.Where(s => !newSectors.Any(ns => ns.Id == s.Id)).ToList();
            foreach (var sector in sectorsToRemove)
            {
                _sectors.Remove(sector);
            }

            foreach (var sector in newSectors)
            {
                if (!_sectors.Any(s => s.Id == sector.Id))
                {
                    _sectors.Add(sector);
                }
            }
        }
    }
}
