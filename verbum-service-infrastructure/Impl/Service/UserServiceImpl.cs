using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using verbum_service_application.Service;
using verbum_service_domain.Common;
using verbum_service_domain.Common.ErrorModel;
using verbum_service_domain.DTO.Request;
using verbum_service_domain.DTO.Response;
using verbum_service_domain.Models;
using verbum_service_domain.Utils;
using verbum_service_infrastructure.DataContext;

namespace verbum_service_infrastructure.Impl.Service
{
    public class UserServiceImpl : UserService
    {
        private readonly verbumContext context;
        private readonly TokenService tokenService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly MailService mailService;
        public UserServiceImpl(verbumContext context, TokenService tokenService, IHttpContextAccessor httpContextAccessor, MailService mailService)
        {
            this.context = context;
            this.tokenService = tokenService;
            this.httpContextAccessor = httpContextAccessor;
            this.mailService = mailService;
        }
        public async Task SignUp(User user)
        {
            context.Users.Add(user);
            await context.SaveChangesAsync();
            await SendConfirmationEmail(user.Email);
        }
        public async Task SendConfirmationEmail(string email)
        {
            var emailToken = Guid.NewGuid().ToString();
            //add cookie
            httpContextAccessor.HttpContext.Response.Cookies.Append(emailToken, email, new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddDays(2),
                HttpOnly = true,
                Secure = true
            });
            //send mail
            string body = await BuildVerificationEmail(SystemConfig.DOMAIN + "auth/confirm-email/" + emailToken + "/" + email);
            await mailService.SendEmailAsync(email, string.Format(MailConstant.SUBJECT, email), body);
        }
        private async Task<string> BuildVerificationEmail(string link)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(),"..", "verbum-service-domain", "Common", "HTMLTemplate", "mail.html");
            string body = await File.ReadAllTextAsync(path);
            body = body.Replace("{link}", link);
            return body;
        }
        public async Task<Tokens> Login(UserLogin loginCredentials)
        {
            List<string> alerts = new List<string>();

            string hashPassword = UserUtils.HashPassword(loginCredentials.Password);
            User user = await context.Users.FirstOrDefaultAsync(x => x.Password == hashPassword && x.Email == loginCredentials.Email);
            if (ObjectUtils.IsEmpty(user) || user.Status != UserStatus.ACTIVE.ToString())
            {
                alerts.Add(AlertMessage.Alert(ValidationAlertCode.NOT_FOUND, "user"));
            }
            if (ObjectUtils.IsNotEmpty(alerts))
            {
                throw new BusinessException(alerts);
            }
            Tokens newTokens = tokenService.GenerateTokens(user);

            //transaction to roll back if update failed
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    await tokenService.UpdateRefreshToken(user.TokenId ?? 0, newTokens.RefreshToken);
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }

            return newTokens;
        }
        public async Task<Tokens> RefreshAccessToken(Tokens tokens)
        {
            List<string> alerts = new List<string>();

            var principal = tokenService.GetPrincipalFromExpiredToken(tokens.AccessToken);
            if (ObjectUtils.IsEmpty(principal))
            {
                alerts.Add(AlertMessage.Alert(ValidationAlertCode.NOT_FOUND, "principal"));
            }
            string? email = principal.FindFirst(ClaimTypes.Email)?.Value;
            User? user = context.Users.Include(x => x.Token).FirstOrDefault(context => context.Email == email);
            if (ObjectUtils.IsEmpty(user))
            {
                alerts.Add(AlertMessage.Alert(ValidationAlertCode.NOT_FOUND, "user"));
            }
            if (user.Token.ExpireAt < DateTime.Now || tokens.RefreshToken != user.Token.TokenContent)
            {
                alerts.Add(AlertMessage.Alert(ValidationAlertCode.INVALID, "refresh token"));
            }
            if (ObjectUtils.IsNotEmpty(alerts))
            {
                throw new BusinessException(alerts);
            }
            Tokens newTokens = tokenService.GenerateTokens(user);
            await tokenService.UpdateRefreshToken(user.TokenId ?? 0, newTokens.RefreshToken);
            return newTokens;
        }

        public async Task<Tokens> ConfirmEmail(string token, string email)
        {
            User user = await context.Users.FirstOrDefaultAsync(x => x.Email == email);
            if(user == null)
            {
                throw new BusinessException(AlertMessage.Alert(ValidationAlertCode.NOT_FOUND, "email"));
            }
            user.EmailVerified = DateTime.Now;
            user.Status = UserStatus.ACTIVE.ToString();
            Tokens tokens = tokenService.GenerateTokens(user);
            user.TokenId = await tokenService.AddRefreshToken(tokens.RefreshToken);
            await context.SaveChangesAsync();
            return tokens;
        }
    }
}
