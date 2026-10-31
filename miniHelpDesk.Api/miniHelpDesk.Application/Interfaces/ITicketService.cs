using miniHelpDesk.Application.DTOs.Requests;
using miniHelpDesk.Application.DTOs.Response;

namespace miniHelpDesk.Application.Interfaces
{
    public interface ITicketService
    {
        Task<List<TicketSummaryResponse>> GetAllAsync();

        Task<TicketDetailResponse> GetByIdAsync(int id);

        Task<TicketSummaryResponse> CreateAsync(CreateTicketRequest data);

        Task<TicketDetailResponse> UpdateTicketProgressAsync(int id, UpdateTicketProgressRequest data);

        Task<TicketSummaryResponse> TransferTicketAsync(int id, TransferTicketRequest data);

        Task<TicketSummaryResponse> AssignAsync(int id);

        Task SaveChangesAsync();
    }
}
