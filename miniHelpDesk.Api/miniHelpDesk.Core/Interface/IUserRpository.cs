using miniHelpDesk.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace miniHelpDesk.Core.Interface
{
    public interface IUserRpository
    {
        Task<User?> GetByIdAsync(int id);

        Task<User?> GetByEmailAsync(string email);

        Task<IEnumerable<User>> GetAllAsync();

        Task<User> CreateAsync(User user);

        Task<User> UpdateAsync(int id, User user);

        Task<bool> DeleteAsync(int id);

        Task<bool> ExistsByEmailAsync(string email);

        Task SaveChangesAsync();

    }
}
