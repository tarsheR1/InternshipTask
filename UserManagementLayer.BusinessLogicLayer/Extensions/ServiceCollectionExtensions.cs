using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using UserManagementService.BusinessLogicLayer.Interfaces.Auth;
using UserManagementService.BusinessLogicLayer.Interfaces.Infrastructure;
using UserManagementService.BusinessLogicLayer.Interfaces.Users;
using UserManagementService.BusinessLogicLayer.Services.ApplicationServices.Auth;
using UserManagementService.BusinessLogicLayer.Services.ApplicationServices.Users;
using UserManagementService.BusinessLogicLayer.Services.ExternalServices;
using Mapster;
using System.Reflection;

namespace UserManagementService.BusinessLogicLayer.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserRoleService, UserRoleService>();
            services.AddScoped<IRolePermissionAssignmentService, RolePermissionAssignmentService>();

            services.AddScoped<IPasswordHasher, PasswordHasher>();

            return services;
        }
    }
}