using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using miniHelpDesk.Application.DTOs.Requests;
using miniHelpDesk.Application.DTOs.Response;
using miniHelpDesk.Application.Interfaces;
using miniHelpDesk.Core.Entities;

namespace miniHelpDesk.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AuthService(IConfiguration configuration, ITokenService tokenService, IUserService userService, IPasswordHasher<User> passwordHasher)
        {
            _configuration = configuration;
            _tokenService = tokenService;
            _userService = userService;
            _passwordHasher = passwordHasher;
        }

        public async Task<AuthResponse?> Auth(AuthRequest request)
        {
            var user = await _userService.GetByEmailAsync(request.Email);

            if (user == null)
                return null;

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, request.Password);
            if (result == PasswordVerificationResult.Failed)
                return null;

            var token = _tokenService.Generate(user);
            return new AuthResponse
            {
                Token = token,
                ExpiresAt = GetExpirationUtc()
            };
        }

        public DateTime GetExpirationUtc()
        {
            var minutes = int.Parse(_configuration["Jwt:ExpirationMinutes"] ?? "60");
            return DateTime.UtcNow.AddMinutes(minutes);
        }
    }
}
