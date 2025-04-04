using MediatR;
using TicketManagementService.Application.Queries;
using TicketManagementService.Domain.Aggregates;
using TicketManagementService.Domain.Interfaces.Repositories;

namespace TicketManagementService.Application.UseCases
{
    public class GetTicketInventoriesForEventQueryHandler
        : IRequestHandler<GetTicketInventoriesForEventQuery,IReadOnlyList<TicketInventory>>
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
            if (query.eventId == Guid.Empty)
                throw new ArgumentException("Invalid event");

            var ticketInventories = await _readRepository
                .GetTicketInventoriesByEventId(query.eventId);

            return ticketInventories ?? new List<TicketInventory>().AsReadOnly();
        }
    }
}