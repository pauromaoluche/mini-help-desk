using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using miniHelpDesk.Application.DTOs.Requests;
using miniHelpDesk.Application.DTOs.Response;
using miniHelpDesk.Application.Interfaces;

namespace miniHelpDesk.Api.Controllers
{
    [Authorize]
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest data)
        {
            var response = await _userService.CreateAsync(data);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var response = await _userService.GetByIdAsync(id);

            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserSummaryResponse>>> GetAll()
        {
            var response = await _userService.GetAllAsync();
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateUserRequest data)
        {
            var response = await _userService.UpdateAsync(id, data);
            return Ok(response);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _userService.DeleteAsync(id);
            return NoContent();
        }
    }
}
