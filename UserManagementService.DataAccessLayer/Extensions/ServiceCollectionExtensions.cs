using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserManagementService.DataAccessLayer.Persistence;
using Microsoft.EntityFrameworkCore;
using UserManagementService.DataAccessLayer.Interfaces;
using UserManagementService.DataAccessLayer.Interfaces.Repositories.Users;
using UserManagementService.DataAccessLayer.Interfaces.Repositories.Roles;
using UserManagementService.DataAccessLayer.Repositories.Roles;
using UserManagementService.DataAccessLayer.Repositories.Users;

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
           
            
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

            return services;
        }
    }
}
