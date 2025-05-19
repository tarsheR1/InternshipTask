using MediatR;
using EventManagementService.Domain.Models;
using EventManagementService.Application.UseCases.Сommands.Events;
using EventManagementService.Domain.Interfaces.Repositories;

namespace EventManagementService.Application.UseCases.CommandHandlers.Events
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Guid>
    {
        private readonly IEventRepository _eventRepository;

        public CreateEventCommandHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<Guid> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var eventEntity = new EventEntity
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                Date = request.Date,
                Location = request.Location,
                CategoryId = request.CategoryId
            };

            await _eventRepository.AddAsync(eventEntity, cancellationToken);
            return eventEntity.Id;
        }
    }
}