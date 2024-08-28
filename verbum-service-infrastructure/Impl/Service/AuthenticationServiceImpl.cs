using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using verbum_service_application.Service;
using verbum_service_domain.Common;
using verbum_service_domain.DTO.Response;
using verbum_service_domain.Models;

namespace verbum_service_infrastructure.Service
{
    public class AuthenticationServiceImpl : AuthenticationService
    {
        public Tokens GenerateTokens(User userInfo)
        {
            var _config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(ClaimTypes.Email, userInfo.Email),
                new Claim(ClaimTypes.Name, userInfo.Name),
                new Claim(ClaimTypes.Role, userInfo.RoleName),
                new Claim(ClaimEnum.STATUS, userInfo.Status)
            };

            var token = new JwtSecurityToken(
              claims: claims,
              expires: DateTime.Now.AddHours(SystemConfig.ACCESS_TOKEN_LIFE),
              audience: _config["Jwt:Audience"],
              issuer: _config["Jwt:Issuer"],
              signingCredentials: credentials);

            return new Tokens
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = GenerateRefreshToken()
            };
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
