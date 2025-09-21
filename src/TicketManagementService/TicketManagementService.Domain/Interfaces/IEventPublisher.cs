using TicketManagementService.Domain.Common;

namespace TicketManagementService.Domain.Interfaces
{
    public interface IEventPublisher
    {
        Task Publish(DomainEvent domainEvent);
    }
}
