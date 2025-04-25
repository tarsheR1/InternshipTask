using MediatR;

namespace EventManagementService.Application.Handlers.CommandHandlers
{
    
    public class GetTicketInventoriesForEventQueryHandler
        : IRequestHandler<GetTicketInventoriesForEventQuery, IReadOnlyList<TicketInventory>>
    {
        private readonly ITicketInventoryReadRepository _readRepository;

        public GetTicketInventoriesForEventQueryHandler(
            ITicketInventoryReadRepository readRepository)
        {
            _readRepository = readRepository;
        }

        public async Task<IReadOnlyList<TicketInventory>> Handle(
            GetTicketInventoriesForEventQuery query,
            CancellationToken cancellationToken)
        {
            if (query.EventId == Guid.Empty)
                throw new ArgumentException("Invalid event ID");

            var ticketInventories = await _readRepository
                .GetTicketInventoriesByEventId(query.EventId);

            return ticketInventories ?? new List<TicketInventory>().AsReadOnly();
        }
    }
}