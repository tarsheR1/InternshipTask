using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using TicketManagementService.Domain.Interfaces.Repositories.Read;
using TicketManagementService.Domain.Interfaces.Repositories.Write;
using TicketManagementService.Infrastructure.Persistance.Repositories.Read;
using TicketManagementService.Infrastructure.Persistance.Repositories.Write;

namespace TicketManagementService.Infrastructure.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            // Read Repositories
            services.AddScoped<ITicketInventoryReadRepository>(sp =>
                new TicketInventoryReadRepository(sp.GetRequiredService<IMongoDatabase>()));

            services.AddScoped<ITicketReadRepository>(sp =>
                new TicketReadRepository(sp.GetRequiredService<IMongoDatabase>()));

            // Write Repositories
            services.AddScoped<ITicketInventoryWriteRepository>(sp =>
                new TicketInventoryWriteRepository(sp.GetRequiredService<IMongoDatabase>()));

            services.AddScoped<ITicketWriteRepository>(sp =>
                new TicketWriteRepository(sp.GetRequiredService<IMongoDatabase>()));

            return services;
        }


    }
}
