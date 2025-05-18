using MediatR;
using EventManagementService.Application.DTO.Events;
using EventManagementService.Application.DTO.Pagination;

namespace EventManagementService.Application.UseCases.Queries.Events
{
    public record GetEventsPagedQuery(
        int PageNumber,
        int PageSize,
        int? CategoryId = null
    ) : IRequest<PaginationResponse<EventDto>>;
}
