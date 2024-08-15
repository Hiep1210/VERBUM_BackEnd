using Microsoft.Extensions.DependencyInjection;

namespace verbum_service_application
{
    public static class DependencyInjectionApp
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(DependencyInjectionApp));
            return services;
        }
    }
}
