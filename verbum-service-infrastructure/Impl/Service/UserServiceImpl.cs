using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using verbum_service_application.Repository;
using verbum_service_application.Service;
using verbum_service_domain.Common;
using verbum_service_domain.DTO.Request;
using verbum_service_domain.DTO.Response;
using verbum_service_domain.Models;
using verbum_service_domain.Models.Results;
using verbum_service_domain.Utils;
using verbum_service_infrastructure.DataContext;

namespace verbum_service_infrastructure.Impl.Service
{
    public class UserServiceImpl : UserService
    {
        private readonly verbum_dbContext context;
        private readonly AuthenticationService authenticationService;
        public UserServiceImpl(verbum_dbContext context, AuthenticationService authenticationService)
        {
            this.context = context;
            this.authenticationService = authenticationService;
        }
        public async Task<ApiResult<Tokens>> Login(UserLogin loginCredentials)
        {
            if (ObjectUtils.IsEmpty(loginCredentials))
            {
                return new ApiErrorResult<Tokens>("login request is null");
            }
            string hashPassword = UserUtils.HashPassword(loginCredentials.Password);
            User user = await context.Users
                .FirstOrDefaultAsync(x => x.Password == hashPassword && x.Email == loginCredentials.Email);
            if (ObjectUtils.IsEmpty(user) || user.Status != UserStatus.ACTIVE.ToString())
            {
                return new ApiErrorResult<Tokens>("cannot find user");
            }
            return new ApiSuccessResult<Tokens>(authenticationService.GenerateTokens(user));
        }
    }
}
