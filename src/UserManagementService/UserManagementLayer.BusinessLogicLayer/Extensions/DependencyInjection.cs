using Microsoft.Extensions.DependencyInjection;
using UserManagementService.BusinessLogicLayer.Interfaces.Auth;
using UserManagementService.BusinessLogicLayer.Interfaces.Infrastructure;
using UserManagementService.BusinessLogicLayer.Interfaces.Users;
using UserManagementService.BusinessLogicLayer.Services.ApplicationServices.Auth;
using UserManagementService.BusinessLogicLayer.Services.ApplicationServices.Users;
using UserManagementService.BusinessLogicLayer.Services.ExternalServices;

namespace UserManagementService.BusinessLogicLayer.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

            return services;
        }
    }
}
