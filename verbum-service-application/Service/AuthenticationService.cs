using verbum_service_domain.DTO.Response;
using verbum_service_domain.Models;

namespace verbum_service_application.Service
{
    public interface AuthenticationService
    {
        Tokens GenerateTokens(User userinfo);
    }
}
