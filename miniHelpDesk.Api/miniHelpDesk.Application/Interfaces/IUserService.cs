using miniHelpDesk.Application.DTOs.Requests;
using miniHelpDesk.Application.DTOs.Response;
using miniHelpDesk.Core.Entities;

namespace miniHelpDesk.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserSummaryResponse?> GetMeAsync();

        Task<UserSummaryResponse?> GetByIdAsync(int id);

        Task<User?> GetByEmailAsync(string email);

        Task<List<UserSummaryResponse>> GetAllAsync();

        Task<UserSummaryResponse> CreateAsync(CreateUserRequest data);

        Task<UserSummaryResponse> UpdateMeAsync(UpdateUserRequest user);

        Task<UserSummaryResponse> UpdateAsync(int id, UpdateUserRequest user);

        Task<bool> DeleteAsync(int id);

        Task SaveChangesAsync();
    }
}
