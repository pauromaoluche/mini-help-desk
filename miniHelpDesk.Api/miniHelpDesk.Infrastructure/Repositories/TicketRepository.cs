using miniHelpDesk.Core.Entities;
using miniHelpDesk.Core.Interface;
using miniHelpDesk.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace miniHelpDesk.Infrastructure.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly AppDbContext _ticketDbContext;

        public TicketRepository(AppDbContext ticketDbContext)
        {
            _ticketDbContext = ticketDbContext;
        }

        public async Task<Ticket> CreateAsync(Ticket ticket)
        {
            await _ticketDbContext.Tickets.AddAsync(ticket);
            await _ticketDbContext.SaveChangesAsync();
            return ticket;
        }

        public async Task AddHistoryAsync(TicketHistory ticketHistory)
        {
            await _ticketDbContext.TicketHistories.AddAsync(ticketHistory);
        }

        public async Task<List<Ticket>> GetAllAsync()
        {
            return await _ticketDbContext.Tickets.ToListAsync();
        }

        public async Task<Ticket?> GetByIdAsync(int id)
        {
            return await _ticketDbContext.Tickets
                .Include(t => t.AssignedTo)
                .Include(t => t.History)
                    .ThenInclude(h => h.ChangedBy)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _ticketDbContext.SaveChangesAsync();
        }
    }
}
