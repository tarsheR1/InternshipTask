using EventManagementService.Application.Mapping;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EventManagementService.Application.Exceptions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationMappings(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<CategoryMappingProfile>();
                cfg.AddProfile<EventMappingProfile>();
            }, Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
