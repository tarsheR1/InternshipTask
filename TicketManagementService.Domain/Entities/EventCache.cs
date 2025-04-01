using TicketManagementService.Domain.Common;

namespace TicketManagementService.Domain.Entities;

public class EventCache : BaseEntity 
{
    public string OriginalEventId { get; } 
    public string Title { get; private set; }
    public DateTime Date { get; private set; }
    public string Location { get; private set; }
    public DateTime LastUpdatedAt { get; private set; }

    private EventCache() { } 

    public EventCache(string originalEventId, string title, DateTime date, string location)
    {
        OriginalEventId = originalEventId;
        Title = title;
        Date = date;
        Location = location;
        LastUpdatedAt = DateTime.UtcNow;
    }

    public void Update(string title, DateTime date, string location)
    {
        Title = title;
        Date = date;
        Location = location;
        LastUpdatedAt = DateTime.UtcNow;
    }
}