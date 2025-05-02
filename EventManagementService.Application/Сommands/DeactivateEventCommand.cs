using EventManagementService.Domain.Models;
using MediatR;

namespace EventManagementService.Application.Сommands
{
    public class DeactivateEventCommand : IRequest<EventEntity>
    {
        public Guid Id { get; set; }
    }
}
