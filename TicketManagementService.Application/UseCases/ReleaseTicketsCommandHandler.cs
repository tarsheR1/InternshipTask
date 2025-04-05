using MediatR;
using TicketManagementService.Application.Commands;
using TicketManagementService.Domain.Interfaces.Repositories;
using TicketManagementService.Domain.Interfaces;

namespace TicketManagementService.Application.UseCases
{
    public class ReleaseTicketsCommandHandler : IRequestHandler<ReleaseTicketsCommand, Unit>
    {
        private readonly ITicketInventoryReadRepository _readRepository;
        private readonly ITicketInventoryWriteRepository _writeRepository;
        private readonly IEventPublisher _eventPublisher;

        public ReleaseTicketsCommandHandler(
            ITicketInventoryReadRepository readRepository,
            ITicketInventoryWriteRepository writeRepository,
            IEventPublisher eventPublisher)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
            _eventPublisher = eventPublisher;
        }

        public async Task<Unit> Handle(ReleaseTicketsCommand command, CancellationToken ct)
        {
            var inventory = await _readRepository.GetAvailabilityAsync(command.EventId, command.TicketType);
            if (inventory == null)
                //throw new TicketInventoryNotFoundException(command.EventId, command.TicketType);

            inventory.ReleaseTickets(command.Quantity);

            await _writeRepository.UpdateAsync(inventory);

            foreach (var ticketsCreated in inventory.DomainEvents)
            {
                await _eventPublisher.Publish(ticketsCreated);
            }

            return Unit.Value;
        }
    }

}
