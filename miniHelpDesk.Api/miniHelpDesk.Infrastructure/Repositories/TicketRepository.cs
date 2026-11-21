using miniHelpDesk.Core.Entities;
using miniHelpDesk.Core.Filters;
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

        public async Task<List<Ticket>> GetAllAsync(TicketFilter filters)
        {
            var query = _ticketDbContext.Tickets.AsQueryable();

            if (filters.Status.HasValue)
            {
                query = query.Where(t => t.Status == filters.Status.Value);
            }

            if (filters.Category.HasValue)
            {
                query = query.Where(t => t.Category == filters.Category.Value);
            }

            if (filters.Priority.HasValue)
            {
                query = query.Where(t => t.Priority == filters.Priority.Value);
            }

            if (filters.AssignedToId.HasValue)
            {
                query = query.Where(t => t.AssignedToId == filters.AssignedToId.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<Ticket?> GetByIdAsync(int id)
        {
            return await _ticketDbContext.Tickets
                .Include(t => t.AssignedTo)
                .Include(t => t.History)
                    .ThenInclude(h => h.ChangedBy)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<Ticket>> GetByUserIdAsync(int userId)
        {
            return await _ticketDbContext.Tickets
                .Where(t => t.AssignedToId == userId)
                .ToListAsync();
        }

        public async Task<Ticket> CreateAsync(Ticket ticket)
        {
            await _ticketDbContext.Tickets.AddAsync(ticket);
            await _ticketDbContext.SaveChangesAsync();
            return ticket;
        }

        public async Task<Ticket> UpdateAsync(Ticket ticket)
        {
            await _ticketDbContext.SaveChangesAsync();
            return ticket;
        }

        public async Task AddHistoryAsync(TicketHistory ticketHistory)
        {
            await _ticketDbContext.TicketHistories.AddAsync(ticketHistory);
        }

        public async Task SaveChangesAsync()
        {
            await _ticketDbContext.SaveChangesAsync();
        }
    }
}
