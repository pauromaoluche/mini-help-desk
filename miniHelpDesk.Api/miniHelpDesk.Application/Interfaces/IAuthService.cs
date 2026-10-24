using miniHelpDesk.Application.DTOs.Requests;
using miniHelpDesk.Application.DTOs.Response;

namespace miniHelpDesk.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse?> Auth(AuthRequest request);

        DateTime GetExpirationUtc();
    }
}
