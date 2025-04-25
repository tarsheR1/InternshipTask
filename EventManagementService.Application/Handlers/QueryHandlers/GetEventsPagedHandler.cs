using EventManagementService.Application.DTO.Events;
using EventManagementService.Application.DTO.Pagination;
using EventManagementService.Application.Queries;
using EventManagementService.Application.Specification.Events;
using EventManagementService.Domain.Interfaces;
using EventManagementService.Domain.Models;
using MediatR;

namespace EventManagementService.Application.Handlers.QueryHandlers
{
    public class GetEventsPagedHandler : IRequestHandler<GetEventsPagedQuery, PaginationResponse<EventDto>>
    {
        private readonly IEventRepository _eventRepository;

        public GetEventsPagedHandler(IEventRepository repository)
            => _eventRepository = repository;

        public async Task<PaginationResponse<EventDto>> Handle(
            GetEventsPagedQuery request,
            CancellationToken ct)
        {
            var spec = new EventsPagedSpecification(
                request.PageNumber,
                request.PageSize,
                request.CategoryId
            );

            var items = await _eventRepository.GetAsync(spec, ct);
            var totalCount = await _eventRepository.CountAsync(spec, ct); 

            return new PaginationResponse<EventDto>(
                Items: items.ConvertToDto(),
                TotalCount: totalCount,
                PageNumber: request.PageNumber,
                PageSize: request.PageSize
            );
        }
    }
}
