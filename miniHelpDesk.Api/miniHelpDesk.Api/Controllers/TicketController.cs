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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTicketRequest data)
        {
            var response = await _ticketService.CreateAsync(data);

            return Ok(response);
        }

    }
}
