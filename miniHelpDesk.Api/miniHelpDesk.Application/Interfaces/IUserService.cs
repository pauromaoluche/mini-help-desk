using miniHelpDesk.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace miniHelpDesk.Application.Interfaces
{
    public interface IUserService
    {
        Task<User?> GetByIdAsync(int id);

        Task<User?> GetByEmailAsync(string email);

        Task<List<User>> GetAllAsync();

        Task<User> CreateAsync(User user);

        Task<User> UpdateAsync(int id, User user);

        Task<bool> DeleteAsync(int id);

        Task<bool> ExistsByEmailAsync(string email);

        Task SaveChangesAsync();
    }
}
