using miniHelpDesk.Core.Entities;
using miniHelpDesk.Core.Interface;
using miniHelpDesk.Infrastructure.Data;

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

        public async Task SaveChangesAsync()
        {
            await _ticketDbContext.SaveChangesAsync();
        }
    }
}
