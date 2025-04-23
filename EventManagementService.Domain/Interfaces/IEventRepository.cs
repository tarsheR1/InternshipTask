using EventManagementService.Domain.Models;

namespace EventManagementService.Domain.Interfaces
{
    public interface IEventRepository 
    {
        Task AddAsync(EventEntity eventEntity, CancellationToken cancellation);

        Task<EventEntity> GetEventAsync(Guid eventId, CancellationToken cancellation);

        Task<List<EventEntity>> GetAllAsync(CancellationToken cancellation);

        Task UpdateAsync(EventEntity eventEntity, CancellationToken cancellation);

        Task DeleteAsync(Guid eventId, CancellationToken cancellation);

        Task<(List<EventEntity> Events, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize, CancellationToken cancellation);
    }
}
