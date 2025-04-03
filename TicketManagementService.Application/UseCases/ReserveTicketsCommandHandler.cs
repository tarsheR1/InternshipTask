using MediatR;
using TicketManagementService.Application.Commands;

namespace TicketManagementService.Application.UseCases
{
    public class ReserveTicketsCommandHandler : IRequestHandler<ReserveTicketsCommand, Unit>
    {
        private readonly ITicketInventoryRepository _repository;

        public ReserveTicketsCommandHandler(ITicketInventoryRepository repository)
            => _repository = repository;

        public async Task<Unit> Handle(ReserveTicketsCommand command, CancellationToken ct)
        {
            var inventory = await _repository.GetByEventAndTypeAsync(command.EventId, command.TicketType);
            if (inventory == null)
                throw new TicketInventoryNotFoundException(command.EventId, command.TicketType);

            inventory.ReserveTickets(command.Quantity);
            await _repository.UpdateAsync(inventory);

            return Unit.Value;
        }
    }
}
