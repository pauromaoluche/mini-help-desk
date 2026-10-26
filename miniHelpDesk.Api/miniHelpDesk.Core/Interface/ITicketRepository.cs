using miniHelpDesk.Core.Entities;

namespace miniHelpDesk.Core.Interface
{
    public interface ITicketRepository
    {
        Task<Ticket> CreateAsync(Ticket ticket);

        Task<List<Ticket>> GetAllAsync();

        Task<Ticket?> GetByIdAsync(int id);

        Task SaveChangesAsync();

    }
}
