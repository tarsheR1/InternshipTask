using EventManagementService.Application.DTO;
using EventManagementService.Application.UseCases.Queries.Events;
using EventManagementService.Domain.Interfaces.Repositories;
using EventManagementService.Domain.Pagination;
using MediatR;

namespace EventManagementService.Application.UseCases.QueryHandlers.Events
{
    public class GetEventsPagedHandler : IRequestHandler<GetEventsQuery, PaginationResponse<EventDto>>
    {
        private readonly IEventRepository _eventRepository;

        public GetEventsPagedHandler(IEventRepository repository)
            => _eventRepository = repository;

        public async Task<PaginationResponse<EventDto>> Handle(
            GetEventsQuery request,
            CancellationToken ct)
        {
            var paginationOptions = new PaginationOptions
            {
                
            };

            var items = await _eventRepository.GetListBySpecAsync(spec, ct);
            var totalCount = await _eventRepository.СountBySpecAsync(spec, ct); 

            return new PaginationResponse<EventDto>(
                Items: items.ConvertToDto(),
                TotalCount: totalCount,
                PageNumber: request.PageNumber,
                PageSize: request.PageSize
            );
        }
    }
}
