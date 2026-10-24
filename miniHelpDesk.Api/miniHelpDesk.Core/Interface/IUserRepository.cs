using miniHelpDesk.Core.Entities;

namespace miniHelpDesk.Core.Interface
{
    public interface IUserRepository
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
