using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using miniHelpDesk.Application.DTOs.Requests;
using miniHelpDesk.Application.Interfaces;
using miniHelpDesk.Core.Entities;

namespace miniHelpDesk.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IAuthService _authService;

        public AuthController(
            IUserService userService,
            IPasswordHasher<User> passwordHasher,
            IAuthService authService)
        {
            _userService = userService;
            _passwordHasher = passwordHasher;
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] AuthRequest request)
        {
            var token = await _authService.Auth(request);

            return token is null ? Unauthorized() : Ok(token);
        }
    }
}
