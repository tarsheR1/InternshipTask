using Microsoft.Extensions.DependencyInjection;
using UserManagementService.DataAccessLayer.Interfaces.Repositories;
using UserManagementService.DataAccessLayer.Repositories;

namespace UserManagementService.DataAccessLayer.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IUserRepository, UserRepository>(); 

            return services;
        }
    }
}
