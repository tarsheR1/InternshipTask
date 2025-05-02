using EventManagementService.Domain.Models;
using MediatR;

namespace EventManagementService.Application.Сommands
{
    public class ApproveEventCommand : IRequest<EventEntity>
    {
        public Guid Id { get; set; }
    }
}
