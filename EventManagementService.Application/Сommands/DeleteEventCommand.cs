using EventManagementService.Domain.Models;
using MediatR;

namespace EventManagementService.Application.Сommands
{
    public class DeleteEventCommand : IRequest<EventEntity> 
    {
        public Guid Id { get; set; }
    }
}

