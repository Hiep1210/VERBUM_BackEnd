using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using verbum_service_domain.DTO.Request;
using verbum_service_domain.DTO.Response;
using verbum_service_domain.Models.Results;

namespace verbum_service_application.Service
{
    public interface UserService
    {
        Task<ApiResult<Tokens>> Login(UserLogin loginCredentials);
        Task<ApiResult<Tokens>> RefreshAccessToken(Tokens tokens);
    }
}
