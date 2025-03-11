using EventManagementService.Application.Queries;
using EventManagementService.Domain.Interfaces;
using EventManagementService.Domain.Models;
using MediatR;

namespace EventManagementService.Application.Handlers.QueryHandlers
{
    public class GetPagedEventsQueryHandler : IRequestHandler<GetPagedEventsQuery, (List<EventEntity> Events, int TotalCount)>
    {
        private readonly IEventRepository _eventRepository;

        public GetPagedEventsQueryHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<(List<EventEntity> Events, int TotalCount)> Handle(GetPagedEventsQuery request, CancellationToken cancellationToken)
        {
            return await _eventRepository.GetPagedAsync(request.PageNumber, request.PageSize);
        }
    }
}
