using miniHelpDesk.Application.DTOs.Requests;
using miniHelpDesk.Application.DTOs.Response;
using miniHelpDesk.Core.Entities;

namespace miniHelpDesk.Application.Interfaces
{
    public interface IUserService
    {
        Task<User?> GetByIdAsync(int id);

        Task<User?> GetByEmailAsync(string email);

        Task<List<User>> GetAllAsync();

        Task<UserSummaryResponse> CreateAsync(CreateUserRequest data);

        Task<User> UpdateAsync(int id, User user);

        Task<bool> DeleteAsync(int id);

        Task<bool> ExistsByEmailAsync(string email);

        Task SaveChangesAsync();
    }
}
