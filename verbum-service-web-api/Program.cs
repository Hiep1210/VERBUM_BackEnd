using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;
using verbum_service_application;
using verbum_service_application.Workflow;
using verbum_service_domain.Common;
using VNH.Infrastructure;

namespace verbum_service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddApplication();

            // Add services to the container.

            builder.Services.AddControllers().AddOData(opt => opt
            .Select()
            .Expand()
            .Filter()
            .OrderBy()
            .SetMaxTop(100)
            ).AddJsonOptions(options =>
            options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(1800);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigins",
                    builder =>
                    {
                        builder.WithOrigins("https://localhost:3000") // URL của frontend
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });
            builder.Services.AddSwaggerGen();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                }
            });
            });
            //add jwt setting 
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Bearer";
                options.DefaultChallengeScheme = "Bearer";
                options.DefaultSignInScheme = "Cookies";
            })
            .AddCookie("Cookies")
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                ClockSkew = TimeSpan.Zero
            };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        if (!string.IsNullOrEmpty(accessToken))
                        {
                            // Attach the token from the query string
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        var claimsPrincipal = context.Principal;
                        if (claimsPrincipal.Identity.IsAuthenticated)
                        {
                            // Populate RequestInfo based on the validated claims
                            var currentUser = context.HttpContext.RequestServices.GetRequiredService<CurrentUser>();
                            currentUser.Id = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";
                            currentUser.Email = claimsPrincipal.FindFirst(ClaimTypes.Email)?.Value ?? "";
                            currentUser.Name = claimsPrincipal.FindFirst(ClaimTypes.Name)?.Value ?? "";
                            currentUser.Status = claimsPrincipal.FindFirst("STATUS")?.Value ?? ""; 
                        }

                        return Task.CompletedTask;
                    }
                    //,
                    //OnAuthenticationFailed = context =>
                    //{
                    //    if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                    //    {
                    //        var exception = (SecurityTokenExpiredException)context.Exception;

                    //        // Log or return custom response for expired token
                    //        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    //        context.Response.ContentType = "application/json";
                    //    }
                    //    return Task.CompletedTask;
                    //}
                };
            })
            .AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"];
                googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
            });

            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddControllers(options =>
            {
                options.Filters.Add<ControllerExceptionFilter>();
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseSession();
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Run();
        }
    }
}
