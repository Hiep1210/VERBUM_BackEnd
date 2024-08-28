using System.Security.Claims;
using verbum_service_domain.DTO.Response;
using verbum_service_domain.Models;

namespace verbum_service_application.Service
{
    public interface TokenService
    {
        Task<int> AddRefreshToken(string token);
        Task UpdateRefreshToken(int tokenId, string newToken);
        Tokens GenerateTokens(User userinfo);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);

    }
}
