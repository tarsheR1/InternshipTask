using EventManagementService.Domain.Entities;
using EventManagementService.Domain.Pagination;

namespace EventManagementService.Domain.Interfaces.Repositories
{
    public interface IEventRepository : IRepository<EventEntity, Guid>
    {
    }
}