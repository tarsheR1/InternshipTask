using EventManagementService.Application.UseCases.Сommands.Events;
using EventManagementService.Domain.Interfaces.Repositories;
using MediatR;

namespace EventManagementService.Application.UseCases.CommandHandlers.Events
{
    class ApproveEventCommandHandler : IRequestHandler<UpdateEventCommand, Guid>
    {
        private readonly IEventRepository _eventRepository;

        public ApproveEventCommandHandler(IEventRepository eventRepository)
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
            eventEntity.Categories = request.Categories;

            await _eventRepository.UpdateAsync(eventEntity, cancellationToken);

            return eventEntity.Id;
        }
    }
}
