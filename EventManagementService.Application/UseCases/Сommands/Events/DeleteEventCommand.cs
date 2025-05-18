using EventManagementService.Domain.Models;
using MediatR;

namespace EventManagementService.Application.UseCases.Сommands.Events
{
    public class DeleteEventCommand : IRequest<EventEntity> 
    {
        public Guid Id { get; set; }
    }
}

