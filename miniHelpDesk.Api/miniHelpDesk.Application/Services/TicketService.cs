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

        public async Task<TicketSummaryResponse> AssignAsync(int id)
        {
            var ticket = await _ticketRepository.GetByIdAsync(id);

            if(ticket == null)
            {
                throw new Exception("Ticket not found");
            }
            if(ticket.AssignedToId != null)
            {
                throw new Exception("Ticket already assigned");
            }

            var userRequest = _httpContextAccessor.HttpContext?.User;

            var idClaim = userRequest?.FindFirst("id")?.Value;

            int.TryParse(idClaim, out var userId);

            var assigned = ticket.AssignTo(userId);

            if (!assigned)
            {
                throw new Exception("Ticket already assigned to this user");
            }

            return _mapper.Map<TicketSummaryResponse>(ticket);
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

        public async Task<List<TicketSummaryResponse>> GetAllAsync()
        {
            var tickets = await _ticketRepository.GetAllAsync();
            return _mapper.Map<List<TicketSummaryResponse>>(tickets);
        }

        public async Task<TicketDetailResponse> GetByIdAsync(int id)
        {
            var ticket = await _ticketRepository.GetByIdAsync(id);
            return _mapper.Map<TicketDetailResponse>(ticket);
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
