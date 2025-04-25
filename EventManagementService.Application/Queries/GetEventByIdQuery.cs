using MediatR;
using EventManagementService.Domain.Models;

namespace EventManagementService.Application.Queries
{
    public class GetEventByIdQuery : IRequest<EventEntity>
    {
        public Guid Id;
    }
}
