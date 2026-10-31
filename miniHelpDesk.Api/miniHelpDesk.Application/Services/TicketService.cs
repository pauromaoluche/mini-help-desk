using AutoMapper;
using Microsoft.AspNetCore.Http;
using miniHelpDesk.Application.common;
using miniHelpDesk.Application.DTOs.Requests;
using miniHelpDesk.Application.DTOs.Response;
using miniHelpDesk.Application.Interfaces;
using miniHelpDesk.Core.Entities;
using miniHelpDesk.Core.Entities.Enums;
using miniHelpDesk.Core.Interface;
using System.Security.Claims;

namespace miniHelpDesk.Application.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IUserRepository _userRepository;
        private readonly ISectorRepository _sectorRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public TicketService(
            ITicketRepository ticketRepository,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper,
            IUserRepository userRepository,
            ISectorRepository sectorRepository)
        {
            _ticketRepository = ticketRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _userRepository = userRepository;
            _sectorRepository = sectorRepository;

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

        public async Task<TicketSummaryResponse> CreateAsync(CreateTicketRequest data)
        {
            var userId = GetCurrentUserId();

            var sector = await _sectorRepository.GetByIdAsync(data.SectorId);
            if (sector == null)
                throw new ApiException(404, "sector_not_found", "Sector not found.");

            var ticket = new Ticket(
               data.Title,
               data.Description,
               data.Category,
               data.Priority,
               userId,
               data.SectorId
            );

            await _ticketRepository.CreateAsync(ticket);

            return _mapper.Map<TicketSummaryResponse>(ticket);
        }

        public async Task<TicketDetailResponse> UpdateTicketProgressAsync(int id, UpdateTicketProgressRequest data)
        {
            var ticket = await _ticketRepository.GetByIdAsync(id);


            if (ticket == null)
                throw new ApiException(404, "ticket_not_found", "Ticket not found.");

            var oldStatus = ticket.Status;

            var updated = ticket.UpdateProgress(data.Status, data.Category, data.Priority, ticket.SectorId, ticket.AssignedToId);

            if (!updated)
                throw new ApiException(400, "ticket_not_updated", "No changes were made to the ticket.");

            await CreateHistory(ticket, ticket.AssignedToId, oldStatus, data.Comment);
            await _ticketRepository.UpdateAsync(ticket);

            return _mapper.Map<TicketDetailResponse>(ticket);
        }

        public async Task<TicketSummaryResponse> TransferTicketAsync(int id, TransferTicketRequest data)
        {
            var ticket = await _ticketRepository.GetByIdAsync(id);

            if (ticket == null)
                throw new ApiException(404, "ticket_not_found", "Ticket not found.");

            var userRole = GetLoggedUserRole();
            var currentUserId = GetCurrentUserId();

            if (userRole == UserRoleEnum.User)
            {
                if (ticket.AssignedToId != currentUserId)
                {
                    throw new ApiException(403, "forbidden_transfer", "Você não tem permissão para transferir um atendimento que não está atribuído a você.");
                }
            }

            var oldAssignedToId = ticket.AssignedToId;
            var oldStatus = ticket.Status;

            int? newAssignedId = null;

            if (data.NewAssignedToId.HasValue)
            {
                if (data.NewAssignedToId.Value > 0)
                {
                    var newAssigned = await _userRepository.GetByIdAsync(data.NewAssignedToId.Value);

                    if (newAssigned == null)
                        throw new ApiException(404, "user_not_found", "User not found.");

                    if (ticket.AssignedToId == newAssigned.Id)
                        throw new ApiException(400, "ticket_already_assigned", "Ticket is already assigned to this user");

                    newAssignedId = newAssigned.Id;
                }
                else
                {
                    newAssignedId = null;
                }
            }

            int targetSectorId = ticket.SectorId;
            if (data.SectorId.HasValue && data.SectorId.Value > 0)
            {
                var sector = await _sectorRepository.GetByIdAsync(data.SectorId.Value);
                if (sector == null)
                    throw new ApiException(404, "sector_not_found", "Sector not found.");

                targetSectorId = data.SectorId.Value;
            }

            var updated = ticket.UpdateProgress(
                status: TicketStatusEnum.Backlog,
                category: data.Category,
                priority: data.Priority,
                sectorId: targetSectorId,
                assignedToId: newAssignedId
            );

            if (!updated)
                throw new ApiException(400, "ticket_not_updated", "No changes were made to the ticket");

            await CreateHistory(ticket, oldAssignedToId, oldStatus, data.Comment);

            await _ticketRepository.SaveChangesAsync();

            return _mapper.Map<TicketSummaryResponse>(ticket);
        }

        public async Task<TicketSummaryResponse> AssignAsync(int id)
        {
            var ticket = await _ticketRepository.GetByIdAsync(id);

            if (ticket == null)
                throw new ApiException(404, "ticket_not_found", "Ticket not found.");

            if (ticket.AssignedToId != null)
                throw new ApiException(400, "ticket_already_assigned", "Ticket is already assigned.");

            var userId = GetCurrentUserId();

            var oldAssignedToId = ticket.AssignedToId;
            var oldStatus = ticket.Status;

            var assigned = ticket.AssignTo(userId);

            if (!assigned)
                throw new ApiException(400, "ticket_already_assigned", "Ticket is already assigned.");

            await CreateHistory(ticket, oldAssignedToId, oldStatus);
            await _ticketRepository.SaveChangesAsync();

            return _mapper.Map<TicketSummaryResponse>(ticket);
        }

        public Task SaveChangesAsync()
        {
            return _ticketRepository.SaveChangesAsync();
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

        private int GetCurrentUserId()
        {
            var userRequest = _httpContextAccessor.HttpContext?.User;
            var idClaim = userRequest?.FindFirst("id")?.Value;

            int.TryParse(idClaim, out var userId);
            return userId;
        }

        private UserRoleEnum GetLoggedUserRole()
        {
            var userRequest = _httpContextAccessor.HttpContext?.User;

            var roleClaim = userRequest?.FindFirst(ClaimTypes.Role)?.Value ?? userRequest?.FindFirst("role")?.Value;

            if (Enum.TryParse<UserRoleEnum>(roleClaim, true, out var loggedUserRole))
            {
                return loggedUserRole;
            }

            return UserRoleEnum.User;
        }
    }
}
