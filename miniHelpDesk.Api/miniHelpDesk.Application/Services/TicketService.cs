using AutoMapper;
using Microsoft.AspNetCore.Http;
using miniHelpDesk.Application.DTOs.Requests;
using miniHelpDesk.Application.DTOs.Response;
using miniHelpDesk.Application.Interfaces;
using miniHelpDesk.Core.Entities;
using miniHelpDesk.Core.Interface;

namespace miniHelpDesk.Application.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public TicketService(ITicketRepository ticketRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _ticketRepository = ticketRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }


        public async Task<TicketSummaryResponse> CreateAsync(CreateTicketRequest data)
        {
            var userRequest = _httpContextAccessor.HttpContext?.User;

            var idClaim = userRequest?.FindFirst("id")?.Value;

            int.TryParse(idClaim, out var userId);

            var ticket = new Ticket(
               data.Title,
               data.Description,
               data.Category,
               data.Priority,
               userId
            );

            await _ticketRepository.CreateAsync(ticket);

            return _mapper.Map<TicketSummaryResponse>(ticket);
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
