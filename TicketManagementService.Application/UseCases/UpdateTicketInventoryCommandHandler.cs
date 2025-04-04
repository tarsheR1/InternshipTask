using TicketManagementService.Domain.Interfaces.Repositories;
using TicketManagementService.Domain.Interfaces;
using TicketManagementService.Application.Commands;
using MediatR;
using System.Runtime.Intrinsics.Arm;

namespace TicketManagementService.Application.UseCases
{
    class UpdateTicketInventoryCommandHandler
    {
        private readonly ITicketInventoryReadRepository _readRepository;
        private readonly ITicketInventoryWriteRepository _writeRepository;
        private readonly IEventPublisher _eventPublisher;

        public UpdateTicketInventoryCommandHandler(
            ITicketInventoryReadRepository readRepository,
            ITicketInventoryWriteRepository writeRepository,
            IEventPublisher eventPublisher)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
            _eventPublisher = eventPublisher;
        }

        public async Task<Unit>Handle(UpdateTicketInventoryCommand updateCommand, CancellationToken cancellationToken)
        {
            var inventory = await _readRepository.GetAvailabilityAsync(updateCommand.EventId, updateCommand.TicketType);

            inventory.UpdateTotalQuantity(updateCommand.NewTotalQuantity);
            inventory.UpdateTicketType(updateCommand.TicketType);

            _writeRepository.UpdateAsync(inventory);

            foreach(var domainEvent in inventory.DomainEvents)
            {
                await _eventPublisher.Publish(domainEvent);
            }

            return Unit.Value;
        }

    }
}
