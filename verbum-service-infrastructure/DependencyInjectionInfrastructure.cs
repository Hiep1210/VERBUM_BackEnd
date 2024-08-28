using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using verbum_service_application.Service;
using verbum_service_domain.Common;
using verbum_service_domain.Models.Mail;
using verbum_service_infrastructure.DataContext;
using verbum_service_infrastructure.Impl.Service;

namespace VNH.Infrastructure
{
    public static class DependencyInjectionInfrastructure
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //      .AddCookie(options =>
            //      {
            //          options.LoginPath = "/UserShort/Login";
            //          options.LogoutPath = "/UserShort/Signup";
            //          options.AccessDeniedPath = "/UserShort/Forbidden/";

            //          options.CookieManager = new ChunkingCookieManager();

            //          options.Cookie.HttpOnly = true;
            //          options.Cookie.SameSite = SameSiteMode.None;
            //          options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            //      });
            // Identity settings
            services.Configure<IdentityOptions>(options =>
            {
                options.Lockout.AllowedForNewUsers = false;
            });

            services.AddDbContext<verbumContext>(options =>
            options.UseNpgsql(SystemConfig.CONNECTION_STRING));
            //services.AddScoped<IRepository, EntityRepository>();
            services.AddScoped<UserService, UserServiceImpl>();
            services.AddScoped<TokenService, TokenServiceImpl>();
            // Facebook, Google
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedProto;
            });
            //services.AddAuthentication()
            //.AddGoogle(googleOptions =>
            //{
            //    googleOptions.ClientId = configuration.GetValue<string>("Authentication:Google:AppId");
            //    googleOptions.ClientSecret = configuration.GetValue<string>("Authentication:Google:AppSecret");
            //// googleOptions.CallbackPath = "/signin-google";
            ////googleOptions.AccessDeniedPath = "/Login";
            ////googleOptions.SaveTokens = true;
            //});
            //.AddFacebook(facebookOptions =>
            //{
            //    facebookOptions.AppId = configuration.GetValue<string>("Authentication:Facebook:AppId");
            //    facebookOptions.AppSecret = configuration.GetValue<string>("Authentication:Facebook:AppSecret");
            //   //// facebookOptions.CallbackPath = "/FacebookCallback";
            //   ////facebookOptions.SaveTokens = true;

            //});

            services.AddOptions();
            services.AddSession();
            var mailsettings = configuration.GetSection("MailSettings");
            services.Configure<MailSettings>(mailsettings);
            //services.AddSingleton<ISendMailService, SendMailService>();
            //services.AddSingleton<IStorageService, StorageService>();

            //services.AddScoped<INewsService, NewsService>();

            return services;
        }
    }
}
