using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserManagementService.DataAccessLayer.Interfaces.Repositories;
using UserManagementService.DataAccessLayer.Persistence;
using UserManagementService.DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;

namespace UserManagementService.DataAccessLayer.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDataAccessLayer(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<UserManagementDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

            return services;
        }
    }
}
