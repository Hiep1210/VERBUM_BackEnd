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
using verbum_service_domain.Models.ErrorModel;
using verbum_service_domain.Models.Results;
using verbum_service_domain.Utils;
using verbum_service_infrastructure.DataContext;

namespace verbum_service_infrastructure.Impl.Service
{
    public class UserServiceImpl : UserService
    {
        private readonly verbumContext context;
        private readonly AuthenticationService authenticationService;
        public UserServiceImpl(verbumContext context, AuthenticationService authenticationService)
        {
            this.context = context;
            this.authenticationService = authenticationService;
        }
        public async Task<ApiResult<Tokens>> Login(UserLogin loginCredentials)
        {
            if (ObjectUtils.IsEmpty(loginCredentials))
            {
                return ApiErrorResult<Tokens>.Alert(ValidationAlertCode.REQUIRED, "login credential");
            }
            string hashPassword = UserUtils.HashPassword(loginCredentials.Password);
            User user = await context.Users
                .FirstOrDefaultAsync(x => x.Password == hashPassword && x.Email == loginCredentials.Email);
            if (ObjectUtils.IsEmpty(user) || user.Status != UserStatus.ACTIVE.ToString())
            {
                return ApiErrorResult<Tokens>.Alert(ValidationAlertCode.NOT_FOUND, "user");
            }
            Tokens newTokens = authenticationService.GenerateTokens(user);

            using (var transaction = context.Database.BeginTransaction())
            {
                int newTokenId = await SaveRefreshToken(newTokens.RefreshToken);
                transaction.Commit();
            }

            return ApiSuccessResult<Tokens>.Success(newTokens);
        }

        private async Task<int> SaveRefreshToken (string token)
        {
            Refreshtoken addedToken = new Refreshtoken
            {
                IssueAt = DateTime.Now,
                ExpireAt = DateTime.Now.AddMonths(SystemConfig.REFRESH_TOKEN_LIFE),
                TokenContent = token
            };
            context.Refreshtokens.Add(addedToken);
            await context.SaveChangesAsync();
            return addedToken.TokenId;
        }
    }
}
