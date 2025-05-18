using EventManagementService.Domain.Models;
using MediatR;

namespace EventManagementService.Application.UseCases.Сommands.Events
{
    public class ApproveEventCommand : IRequest<EventEntity>
    {
        public Guid Id { get; set; }
    }
}
