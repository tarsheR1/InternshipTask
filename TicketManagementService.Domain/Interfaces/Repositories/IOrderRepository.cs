using TicketManagementService.Domain.Entities;

namespace TicketManagementService.Domain.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task<Order?> GetByIdAsync(string id);
        Task<IReadOnlyList<Order>> GetByUserIdAsync(string userId);
        Task AddAsync(Order order);
        Task UpdateAsync(Order order);
    }

}
