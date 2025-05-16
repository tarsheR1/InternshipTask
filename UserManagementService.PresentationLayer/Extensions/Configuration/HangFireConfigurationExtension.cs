using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Extensions.Configuration;

namespace UserManagementService.PresentationLayer.Extensions.Configuration
{
    public static class HangfireConfigurationExtensions
    {
        public static IServiceCollection AddCustomHangfire(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var hangfireConnection = configuration.GetConnectionString("HangfireConnection");

            services.AddHangfire(config =>
                config.UseSqlServerStorage(hangfireConnection));

            services.AddHangfireServer();

            return services;
        }
    }
}