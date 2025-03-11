using MediatR;
using EventManagementService.Domain.Interfaces;
using EventManagementService.Domain.Models;
using EventManagementService.Application.Сommands;

namespace EventManagementService.Application.Handlers
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

            await _eventRepository.AddAsync(eventEntity);
            return eventEntity.Id;
        }
    }
}