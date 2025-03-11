using EventManagementService.Application.Сommands;
using EventManagementService.Domain.Interfaces;
using EventManagementService.Domain.Models;
using MediatR;

namespace EventManagementService.Application.Handlers
{
    public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand, EventEntity>
    {
        private readonly IEventRepository _eventRepository;

        public DeleteEventCommandHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<EventEntity> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            var eventEntity = await _eventRepository.GetEventAsync(request.Id);
            if (eventEntity == null)
            {
                throw new KeyNotFoundException($"Мероприятие с ID {request.Id} не найдено.");
            }

            await _eventRepository.DeleteAsync(request.Id);

            return eventEntity; 
        }
    }
}
