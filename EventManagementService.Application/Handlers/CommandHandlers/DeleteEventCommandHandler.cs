using EventManagementService.Application.Сommands;
using EventManagementService.Domain.Interfaces;
using EventManagementService.Domain.Models;
using MediatR;

namespace EventManagementService.Application.Handlers.CommandHandlers
{
    
    public class DeleteEventCommandHandler
        : IRequestHandler<DeleteEventCommand, EventEntity>
    {
        private readonly IEventRepository _eventRepository;

        public DeleteEventCommandHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<Guid> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            var eventEntity = await _eventRepository.GetByIdAsync(request.Id, cancellationToken);
            if (eventEntity == null)
            {
                throw new KeyNotFoundException($"Мероприятие с ID {request.Id} не найдено.");
            }

            await _eventRepository.DeleteAsync(eventEntity, cancellationToken);

            return eventEntity.Id;
        }
    }
}