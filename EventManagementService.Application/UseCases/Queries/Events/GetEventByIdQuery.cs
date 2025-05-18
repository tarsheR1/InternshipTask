using MediatR;
using EventManagementService.Domain.Models;

namespace EventManagementService.Application.UseCases.Queries.Events
{
    public class GetEventByIdQuery : IRequest<EventEntity>
    {
        public Guid Id;
    }
}
