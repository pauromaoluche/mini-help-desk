using miniHelpDesk.Application.DTOs.Requests;
using miniHelpDesk.Application.DTOs.Response;

namespace miniHelpDesk.Application.Interfaces
{
    public interface ITicketService
    {
        Task<TicketSummaryResponse> CreateAsync(CreateTicketRequest data);

        Task<TicketDetailResponse> GetByIdAsync(int id);

        Task<List<TicketSummaryResponse>> GetAllAsync();

        Task<TicketSummaryResponse> AssignAsync(int id);

        Task SaveChangesAsync();
    }
}
