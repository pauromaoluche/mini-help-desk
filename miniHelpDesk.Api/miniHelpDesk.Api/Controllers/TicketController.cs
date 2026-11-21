using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using miniHelpDesk.Application.DTOs.Requests;
using miniHelpDesk.Application.Interfaces;

namespace miniHelpDesk.Api.Controllers
{
    [Authorize]
    [Route("api/tickets")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] TicketFilterRequest filters)
        {
            var response = await _ticketService.GetAllAsync(filters);

            return Ok(response);
        }

        [HttpGet("me/summary")]
        public async Task<IActionResult> GetMyTickets()
        {
            var response = await _ticketService.GetMyTicketsAsync();

            return Ok(response);
        }

        [HttpGet("me/resume")]
        public async Task<IActionResult> GetMyTicketsResume()
        {
            var response = await _ticketService.GetMyTicketsResumeAsync();

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var response = await _ticketService.GetByIdAsync(id);

            return Ok(response);
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTicketRequest data)
        {
            var response = await _ticketService.CreateAsync(data);

            return Ok(response);
        }

        [HttpPatch("{id}/progress")]
        public async Task<IActionResult> UpdateTicketProgress([FromRoute] int id, [FromBody] UpdateTicketProgressRequest data)
        {
            var response = await _ticketService.UpdateTicketProgressAsync(id, data);

            return Ok(response);
        }

        [HttpPatch("{id}/transfer")]
        public async Task<IActionResult> TransferTicket([FromRoute] int id, [FromBody] TransferTicketRequest data)
        {
            var response = await _ticketService.TransferTicketAsync(id, data);
            return Ok(response);
        }

        [HttpPatch("{id}/assign")]
        public async Task<IActionResult> Assign([FromRoute] int id)
        {
            var response = await _ticketService.AssignAsync(id);
            return Ok(response);
        }
    }
}
