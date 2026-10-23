using miniHelpDesk.Application.DTOs.Response;
using miniHelpDesk.Core.Entities;

namespace miniHelpDesk.Application.Interfaces
{
    public interface ITokenService
    {
        string Generate(User user);
    }
}
