using miniHelpDesk.Application.DTOs.Requests;
using miniHelpDesk.Application.DTOs.Response;

namespace miniHelpDesk.Application.Interfaces
{
    public interface ITicketService
    {
        Task<TicketSummaryResponse> CreateAsync(CreateTicketRequest data);

        Task<TicketDetailResponse> GetByIdAsync(int id);

        Task<TicketSummaryResponse> TransferTicketAsync(int id, TransferTicketRequest data);

        Task<List<TicketSummaryResponse>> GetAllAsync();

        Task<TicketSummaryResponse> AssignAsync(int id);

        Task<TicketDetailResponse> UpdateTicketProgressAsync(int id, UpdateTicketProgressRequest data);

        Task SaveChangesAsync();
    }
}
