using MediatR;
using EventManagementService.Application.UseCases.Сommands.Events;
using EventManagementService.Domain.Interfaces.Repositories;
using EventManagementService.Domain.Entities;
using AutoMapper;

namespace EventManagementService.Application.UseCases.CommandHandlers.Events
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Guid>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;

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
                Categories = _mapper.Map<List<CategoryEntity>>(request.Categories)
            };

            await _eventRepository.AddAsync(eventEntity, cancellationToken);
            return eventEntity.Id;
        }
    }
}