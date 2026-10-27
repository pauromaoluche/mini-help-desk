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

        [Authorize(Roles = "Admin,Manager")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTicketRequest data)
        {
            var response = await _ticketService.CreateAsync(data);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var response = await _ticketService.GetByIdAsync(id);

            return Ok(response);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _ticketService.GetAllAsync();

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
