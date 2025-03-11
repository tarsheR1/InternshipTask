using EventManagementService.Application.Сommands;
using EventManagementService.Domain.Interfaces;
using EventManagementService.Domain.Models;
using MediatR;

namespace EventManagementService.Application.Handlers
{
    public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, Guid>
    {
        private readonly IEventRepository _eventRepository;

        public UpdateEventCommandHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<Guid> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            var eventEntity = await _eventRepository.GetEventAsync(request.Id);
            if (eventEntity == null)
            {
                throw new KeyNotFoundException($"Мероприятие с ID {request.Id} не найдено.");
            }

            eventEntity.Title = request.Title;
            eventEntity.Description = request.Description;
            eventEntity.Date = request.Date;
            eventEntity.Location = request.Location;
            eventEntity.CategoryId = request.CategoryId;

            await _eventRepository.UpdateAsync(eventEntity);

            return eventEntity.Id;
        }
    }
}
