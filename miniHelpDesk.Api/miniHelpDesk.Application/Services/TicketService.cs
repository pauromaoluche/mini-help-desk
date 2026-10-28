using AutoMapper;
using Microsoft.AspNetCore.Http;
using miniHelpDesk.Application.DTOs.Requests;
using miniHelpDesk.Application.DTOs.Response;
using miniHelpDesk.Application.Interfaces;
using miniHelpDesk.Core.Entities;
using miniHelpDesk.Core.Entities.Enums;
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

            var userId = GetCurrentUserId();

            var oldAssignedToId = ticket.AssignedToId;
            var oldStatus = ticket.Status;

            var assigned = ticket.AssignTo(userId);

            if (!assigned)
            {
                throw new Exception("Ticket already assigned to this user");
            }

            await CreateHistory(ticket, oldAssignedToId, oldStatus);
            await _ticketRepository.SaveChangesAsync();

            return _mapper.Map<TicketSummaryResponse>(ticket);
        }

        public async Task<TicketSummaryResponse> CreateAsync(CreateTicketRequest data)
        {
            var userId = GetCurrentUserId();

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

        private async Task CreateHistory(
            Ticket ticket,
            int? oldAssignedToId,
            TicketStatusEnum? oldStatus,
            string? comment = null)
        {
            var history = new TicketHistory(
                ticket.Id,
                GetCurrentUserId(),
                oldAssignedToId,
                ticket.AssignedToId,
                oldStatus,
                ticket.Status,
                comment);

            await _ticketRepository.AddHistoryAsync(history);
        }

        public Task SaveChangesAsync()
        {
            return _ticketRepository.SaveChangesAsync();
        }

        private int GetCurrentUserId()
        {
            var userRequest = _httpContextAccessor.HttpContext?.User;
            var idClaim = userRequest?.FindFirst("id")?.Value;

            int.TryParse(idClaim, out var userId);
            return userId;
        }

        public async Task<TicketDetailResponse> UpdateTicketProgressAsync(int id, UpdateTicketProgressRequest data)
        {
            var ticket = await _ticketRepository.GetByIdAsync(id);


            if(ticket == null)
            {
                throw new Exception("Ticket not found");
            }

            var oldStatus = ticket.Status;

            var updated = ticket.UpdateProgress(data.Status);

            if (!updated)
            {
                throw new Exception("No changes were made to the ticket");
            }

            await CreateHistory(ticket, ticket.AssignedToId, oldStatus, data.Comment);
            await _ticketRepository.SaveChangesAsync();

            return _mapper.Map<TicketDetailResponse>(ticket);
        }
    }
}
