using EventManagementService.Application.UseCases.Сommands.Events;
using EventManagementService.Domain.Interfaces;
using EventManagementService.Domain.Models;
using MediatR;

namespace EventManagementService.Application.UseCases.CommandHandlers.Events
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
            var eventEntity = await _eventRepository.GetByIdAsync(request.Id, cancellationToken);
            if (eventEntity == null)
            {
                throw new KeyNotFoundException($"Мероприятие с ID {request.Id} не найдено.");
            }

            eventEntity.Title = request.Title;
            eventEntity.Description = request.Description;
            eventEntity.Date = request.Date;
            eventEntity.Location = request.Location;
            eventEntity.CategoryId = request.CategoryId;

            await _eventRepository.UpdateAsync(eventEntity, cancellationToken);

            return eventEntity.Id;
        }
    }
}
