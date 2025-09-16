using EventManagementService.Application.UseCases.Сommands.Events;
using EventManagementService.Domain.Entities;
using EventManagementService.Domain.Interfaces.Repositories;
using MediatR;

namespace EventManagementService.Application.UseCases.CommandHandlers.Events
{
    
    public class DeleteEventCommandHandler
        : IRequestHandler<DeleteEventCommand, EventEntity>
    {
        private readonly IEventRepository _eventRepository;

        public DeleteEventCommandHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<EventEntity> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            var eventEntity = await _eventRepository.GetByIdAsync(request.Id, cancellationToken);
            if (eventEntity == null)
            {
                throw new KeyNotFoundException($"Мероприятие с ID {request.Id} не найдено.");
            }

            await _eventRepository.DeleteAsync(eventEntity, cancellationToken);

            return eventEntity;
        }
    }
}