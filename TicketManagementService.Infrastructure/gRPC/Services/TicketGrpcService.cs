using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using TicketService.Grpc;
using TicketManagementService.Application.Commands;

namespace TicketManagementService.Infrastructure.gRPC.Services
{
    // Services/TicketGrpcService.cs
    public class TicketGrpcService
    {
        private readonly IMediator _mediator;
        private readonly ILogger<TicketGrpcService> _logger;

        public TicketGrpcService(
            IMediator mediator,
            ILogger<TicketGrpcService> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<CreateTicketTypeResponse> CreateTicketType(
            CreateTicketTypeRequest request,
            ServerCallContext context)
        {
            _logger.LogInformation("Creating ticket type via MediatR");

            var command = new CreateTicket
            {
                EventId = Guid.Parse(request.EventId),
                TicketType = request.TicketType,
                TotalQuantity = request.TotalQuantity
            };

            var result = await _mediator.Send(command, context.CancellationToken);

            return new CreateTicketTypeResponse
            {
                Success = result.Success,
                TicketTypeId = result.TicketTypeId?.ToString() ?? "",
                ErrorMessage = result.ErrorMessage ?? ""
            };
        }
    }
}
