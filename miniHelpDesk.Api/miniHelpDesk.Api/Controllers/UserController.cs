using Microsoft.AspNetCore.Mvc;
using miniHelpDesk.Application.DTOs.Requests;
using miniHelpDesk.Application.Interfaces;
using miniHelpDesk.Core.Entities;

namespace miniHelpDesk.Api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById([FromRoute] int id)
        {
            var response = await _userService.GetByIdAsync(id);

            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest data)
        {
            var response = await _userService.CreateAsync(data);
            
            return Ok(response);
        }
    }
}
