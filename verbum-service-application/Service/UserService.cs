using verbum_service_domain.DTO.Request;
using verbum_service_domain.DTO.Response;
using verbum_service_domain.Models;
using verbum_service_infrastructure.PagedList;

namespace verbum_service_application.Service
{
    public interface UserService
    {
        Task<Tokens> Login(UserLogin loginCredentials);
        Task<Tokens> RefreshAccessToken(Tokens tokens);
        Task SignUp(User user);
        Task<Tokens> ConfirmEmail(string email);
        Task SendConfirmationEmail(string email);
        Task<Tokens> LoginGoogleCallback();
        Task<UserInfo> GetUserInCompanyById(Guid userId, Guid companyId);
        Task<List<UserInfo>> GetAllUserInCompany(Guid companyId);
        Task UpdateUser(UserUpdate userUpdate);
        Task UpdateUserCompanyStatus(Guid userId, Guid companyId);
    }
}
