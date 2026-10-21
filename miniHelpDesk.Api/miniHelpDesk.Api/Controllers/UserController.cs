using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using miniHelpDesk.Application.Interfaces;
using miniHelpDesk.Core.Entities;

namespace miniHelpDesk.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            var result = await _userService.CreateAsync(user);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
