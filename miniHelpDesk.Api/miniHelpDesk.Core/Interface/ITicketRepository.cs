using miniHelpDesk.Core.Entities;

namespace miniHelpDesk.Core.Interface
{
    public interface ITicketRepository
    {
        Task<List<Ticket>> GetAllAsync();

        Task<Ticket?> GetByIdAsync(int id);

        Task<Ticket> CreateAsync(Ticket ticket);
        
        Task<Ticket> UpdateAsync(Ticket ticket);

        Task AddHistoryAsync(TicketHistory ticketHistory);
    
        Task SaveChangesAsync();

    }
}
