using MediatR;
using EventManagementService.Domain.Entities;

namespace EventManagementService.Application.UseCases.Queries.Events
{
    public class GetEventByIdQuery : IRequest<EventEntity>
    {
        public Guid Id;
    }
}
