using MediatR;
using EventManagementService.Domain.Pagination;
using EventManagementService.Application.DTO;

namespace EventManagementService.Application.UseCases.Queries.Events
{
    public record GetEventsQuery(
        int PageNumber,
        int PageSize,
        int? CategoryId = null
    ) : IRequest<PaginationResponse<EventDto>>;
}
