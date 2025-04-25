using MediatR;
using EventManagementService.Application.DTO.
using EventManagementService.Application.DTO.Pagination;

namespace EventManagementService.Application.Queries
{
    public record GetEventsPagedQuery(
        int PageNumber,
        int PageSize,
        int? CategoryId = null
    ) : IRequest<PaginationResponse<Event>>;
}
