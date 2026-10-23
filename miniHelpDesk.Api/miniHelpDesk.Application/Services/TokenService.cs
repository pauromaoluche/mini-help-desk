using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using miniHelpDesk.Application.DTOs.Response;
using miniHelpDesk.Application.Interfaces;
using miniHelpDesk.Core.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace miniHelpDesk.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Generate(User user)
        {
            var handler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!);
            var issuer = _configuration["Jwt:Issuer"]!;
            var audience = _configuration["Jwt:Audience"]!;
            var expirationMinutes = int.Parse(_configuration["Jwt:ExpirationMinutes"] ?? "60");

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = GenerateClaims(user),
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddMinutes(expirationMinutes),
                Issuer = issuer,
                Audience = audience
            };


            var token = handler.CreateToken(tokenDescriptor);

            var strToken = handler.WriteToken(token);

            return strToken;
        }

        private static ClaimsIdentity GenerateClaims(User user)
        {
            var ci = new ClaimsIdentity();

            ci.AddClaim(new Claim("id", user.Id.ToString()));

            ci.AddClaim(new Claim(ClaimTypes.Email, user.Email));

            ci.AddClaim(new Claim(ClaimTypes.Role, user.Role.ToString()));

            return ci;
        }
    }
}
