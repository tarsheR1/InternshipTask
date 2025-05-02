using EventManagementService.Application.Сommands;
using EventManagementService.Domain.Interfaces;
using MediatR;

namespace EventManagementService.Application.Handlers.CommandHandlers
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
            eventEntity.CategoryId = request.CategoryId;

            await _eventRepository.UpdateAsync(eventEntity, cancellationToken);

            return eventEntity.Id;
        }
    }
}
