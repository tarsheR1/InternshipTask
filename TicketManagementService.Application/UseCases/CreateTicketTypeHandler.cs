using MediatR;
using TicketManagementService.Application.Commands;
using TicketManagementService.Application.DTO;
using TicketManagementService.Domain.Aggregates;
using TicketManagementService.Domain.Interfaces.Repositories.Write;

namespace TicketManagementService.Application.UseCases
{
    public class CreateTicketTypeHandler
        : IRequestHandler<CreateTicketTypeCommand, CreateTicketTypeResult>
    {
        private readonly ITicketInventoryWriteRepository _repository;
        public CreateTicketTypeHandler(
            ITicketInventoryWriteRepository context)
        {
            _repository = context;
        }

        public async Task<CreateTicketTypeResult> Handle(
            CreateTicketTypeCommand request,
            CancellationToken cancellationToken)
        {
           var inventory = new TicketInventory
            {
                EventId = request.EventId,
                TicketType = request.TicketType,
                TotalQuantity = request.TotalQuantity,
                AvailableQuantity = request.TotalQuantity,
                ReservedQuantity = 0
            };

            await _repository.AddAsync(inventory);

            return new CreateTicketTypeResult(true, inventory.EventId);
        }
    }
}
