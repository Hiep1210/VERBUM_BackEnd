using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        private readonly TokenService tokenService;
        public UserServiceImpl(verbumContext context, TokenService tokenService)
        {
            this.context = context;
            this.tokenService = tokenService;
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
            Tokens newTokens = tokenService.GenerateTokens(user);

            if (user.TokenId == 0)
            {
                return ApiErrorResult<Tokens>.Alert(ValidationAlertCode.REQUIRED, "token");
            }

            //transaction to roll back if update failed
            using (var transaction = context.Database.BeginTransaction())
            {
                await tokenService.UpdateRefreshToken(user.TokenId ?? 0, newTokens.RefreshToken);
                transaction.Commit();
            }

            return ApiSuccessResult<Tokens>.Success(newTokens);
        }
        public async Task<ApiResult<Tokens>> RefreshAccessToken(Tokens tokens)
        {
            var principal = tokenService.GetPrincipalFromExpiredToken(tokens.AccessToken);
            if(ObjectUtils.IsEmpty(principal))
            {
                return ApiErrorResult<Tokens>.Alert(StatusCodes.Status400BadRequest, ValidationAlertCode.NOT_FOUND, "principal");
            }
            string? email = principal.FindFirst(ClaimTypes.Email)?.Value;
            User? user = context.Users.Include(x => x.Token).FirstOrDefault(context => context.Email == email);
            if(ObjectUtils.IsEmpty(user)) {
                return ApiErrorResult<Tokens>.Alert(StatusCodes.Status400BadRequest, ValidationAlertCode.NOT_FOUND, "user");
            }
            if(user.Token.ExpireAt < DateTime.Now || tokens.RefreshToken != user.Token.TokenContent)
            {
                return ApiErrorResult<Tokens>.Alert(StatusCodes.Status401Unauthorized, ValidationAlertCode.INVALID, "refresh token");
            }
            Tokens newTokens = tokenService.GenerateTokens(user);
            await tokenService.UpdateRefreshToken(user.TokenId ?? 0, newTokens.RefreshToken);
            return ApiSuccessResult<Tokens>.Success(StatusCodes.Status201Created, newTokens);
        }
    }
}
