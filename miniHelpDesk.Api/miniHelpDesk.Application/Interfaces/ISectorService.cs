using miniHelpDesk.Application.DTOs.Response;

namespace miniHelpDesk.Application.Interfaces
{
    public interface ISectorService
    {
        Task<List<SectorSummaryResponse>> GetAllAsync();
    }
}
