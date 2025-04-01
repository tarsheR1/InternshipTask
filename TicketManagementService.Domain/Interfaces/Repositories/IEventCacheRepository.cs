using TicketManagementService.Domain.Entities;

namespace TicketManagementService.Domain.Interfaces.Repositories
{
    public interface IEventCacheRepository
    {
        Task<EventCache?> GetByEventIdAsync(string eventId);

        Task UpsertAsync(EventCache eventCache);

        Task DeleteAsync(string eventId);

        Task<IReadOnlyList<EventCache>> GetUpcomingEventsAsync(DateTime fromDate);
    }
}
