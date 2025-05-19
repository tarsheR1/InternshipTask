using EventManagementService.Domain.Entities;
using MediatR;

namespace EventManagementService.Application.UseCases.Сommands.Events
{
    public class DeactivateEventCommand : IRequest<EventEntity>
    {
        public Guid Id { get; set; }
    }
}
