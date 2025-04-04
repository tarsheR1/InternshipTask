using MediatR;
using TicketManagementService.Application.Commands;
using TicketManagementService.Domain.Exceptions;
using TicketManagementService.Domain.Interfaces;
using TicketManagementService.Domain.Interfaces.Repositories;


namespace TicketManagementService.Application.UseCases
{
    public class ReserveTicketsCommandHandler : IRequestHandler<ReserveTicketsCommand, Unit>
    {
        private readonly ITicketInventoryReadRepository _readRepository;
        private readonly ITicketInventoryWriteRepository _writeRepository;
        private readonly IEventPublisher _eventPublisher;

        public ReserveTicketsCommandHandler(
            ITicketInventoryReadRepository readRepository,
            ITicketInventoryWriteRepository writeRepository,
            IEventPublisher eventPublisher)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
            _eventPublisher = eventPublisher;
        }

        public async Task<Unit> Handle(ReserveTicketsCommand command, CancellationToken cancellationToken)
        {
            var inventory = await _readRepository.GetAvailabilityAsync(command.EventId, command.TicketType);
            if (inventory == null)
                //Прописать Exception
            // throw new DomainException()
            inventory.ReserveTickets(command.Quantity, command.UserId);
            await _writeRepository.UpdateAsync(inventory);

            foreach (var domainEvent in inventory.DomainEvents)
            {
                await _eventPublisher.Publish(domainEvent);
            }
            inventory.ClearDomainEvents();

            return Unit.Value;
        }
    }
}
