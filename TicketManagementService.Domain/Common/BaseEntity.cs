namespace TicketManagementService.Domain.Common
{
    public abstract class BaseEntity
    {
        public string Id { get; protected init; } = Guid.NewGuid().ToString();

    }
}
