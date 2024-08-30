using Microsoft.Extensions.DependencyInjection;
using verbum_service_application.Mapper;

namespace verbum_service_application
{
    public static class DependencyInjectionApp
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            //dependency inject Imapper
            services.AddAutoMapper(typeof(MyMapper).Assembly);
            return services;
        }
    }
}
