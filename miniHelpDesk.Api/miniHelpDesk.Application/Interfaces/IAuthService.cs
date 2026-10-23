using miniHelpDesk.Application.DTOs.Requests;
using miniHelpDesk.Application.DTOs.Response;
using miniHelpDesk.Core.Entities;

namespace miniHelpDesk.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse?> Auth(AuthRequest request);
        string GenerateToken(User user);
        DateTime GetExpirationUtc();
    }
}
