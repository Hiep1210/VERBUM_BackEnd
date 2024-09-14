using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using verbum_service_application.Service;
using verbum_service_application.Workflow;
using verbum_service_domain.Common;
using verbum_service_domain.DTO.Request;
using verbum_service_domain.Models;
using verbum_service_domain.Models.Mail;
using verbum_service_infrastructure.DataContext;
using verbum_service_infrastructure.Impl.Service;
using verbum_service_infrastructure.Impl.Validation;
using verbum_service_infrastructure.Impl.Workflow;

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
            //services.AddIdentity<User, Role>()
            //    .AddEntityFrameworkStores<verbumContext>()
            //.AddDefaultTokenProviders();

            //service dependency
            services.AddDbContext<verbumContext>(options =>
            options.UseNpgsql(SystemConfig.CONNECTION_STRING));
            services.AddScoped<CurrentUser>();
            services.AddScoped<UserService, UserServiceImpl>();
            services.AddScoped<TokenService, TokenServiceImpl>();
            services.AddScoped<CompanyService, CompanyServiceImpl>();

            //workflow dependency
            services.AddScoped<CreateUserWorkflow>();
            services.AddScoped<UpdateUserWorkflow>();
            services.AddScoped<CreateCompanyWorkflow>();

            //validation dependency
            services.AddScoped<UserSignUpValidation>();
            services.AddScoped<UserUpdateValidation>();

            services.AddHttpContextAccessor();

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
            var mailsettings = configuration.GetSection("MailSettings");
            services.Configure<MailSettings>(mailsettings);
            services.AddTransient<MailService, MailServiceImpl>();
            //services.AddSingleton<IStorageService, StorageService>();

            //services.AddScoped<INewsService, NewsService>();

            return services;
        }
    }
}
