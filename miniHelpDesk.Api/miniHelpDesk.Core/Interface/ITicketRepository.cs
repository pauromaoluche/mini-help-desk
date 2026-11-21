using miniHelpDesk.Core.Entities;
using miniHelpDesk.Core.Filters;

namespace miniHelpDesk.Core.Interface
{
    public interface ITicketRepository
    {
        Task<List<Ticket>> GetAllAsync(TicketFilter filters);

        Task<Ticket?> GetByIdAsync(int id);

        Task<List<Ticket>> GetByUserIdAsync(int userId);

        Task<Ticket> CreateAsync(Ticket ticket);
        
        Task<Ticket> UpdateAsync(Ticket ticket);

        Task AddHistoryAsync(TicketHistory ticketHistory);
    
        Task SaveChangesAsync();

    }
}
