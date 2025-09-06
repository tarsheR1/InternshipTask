using MediatR;
using TicketManagementService.Application.Commands;
using TicketManagementService.Domain.Interfaces;
using TicketManagementService.Domain.Interfaces.Repositories.Read;
using TicketManagementService.Domain.Interfaces.Repositories.Write;

namespace TicketManagementService.Application.UseCases
{
    public class ReleaseTicketsCommandHandler : IRequestHandler<ReleaseTicketCommand, Unit>
    {
        private readonly ITicketReadRepository _readRepository;
        private readonly ITicketWriteRepository _writeRepository;
        private readonly IEventPublisher _eventPublisher;

        public ReleaseTicketsCommandHandler(
            ITicketReadRepository readRepository,
            ITicketWriteRepository writeRepository,
            IEventPublisher eventPublisher)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
            _eventPublisher = eventPublisher;
        }

        public async Task<Unit> Handle(ReleaseTicketCommand command, CancellationToken cancellationToken)
        {
            var ticketExists = await _readRepository.ExistsAsync(command.TicketId, cancellationToken);
            if (ticketExists == false)
            {
                //throw new TicketInventoryNotFoundException(command.EventId, command.TicketType);

            }

            _writeRepository.DeleteAsync(command.TicketId, cancellationToken);

            // RETURN TICKET

            return Unit.Value;
        }
    }

}
