using MediatR;
using TicketManagementService.Application.Commands;

namespace TicketManagementService.Application.UseCases
{
    public class ReleaseTicketsCommandHandler : IRequestHandler<ReleaseTicketsCommand, Unit>
    {
        private readonly ITicketInventoryRepository _repository;

        public ReleaseTicketsCommandHandler(ITicketInventoryRepository repository)
            => _repository = repository;

        public async Task<Unit> Handle(ReleaseTicketsCommand command, CancellationToken ct)
        {
            var inventory = await _repository.GetByEventAndTypeAsync(command.EventId, command.TicketType);
            if (inventory == null)
                throw new TicketInventoryNotFoundException(command.EventId, command.TicketType);

            inventory.ReleaseTickets(command.Quantity);
            await _repository.UpdateAsync(inventory);

            return Unit.Value;
        }
    }

}
