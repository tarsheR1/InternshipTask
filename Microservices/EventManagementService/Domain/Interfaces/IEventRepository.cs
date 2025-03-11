using EventManagementService.Domain.Models;

namespace EventManagementService.Domain.Interfaces
{
    public interface IEventRepository 
    {
        Task AddAsync(EventEntity eventEntity);
        Task<EventEntity> GetEventAsync(Guid eventId);
        Task<List<EventEntity>> GetAllAsync();
        Task UpdateAsync(EventEntity eventEntity);
        Task DeleteAsync(Guid eventId);

        Task<(List<EventEntity> Events, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize);
    }
}
