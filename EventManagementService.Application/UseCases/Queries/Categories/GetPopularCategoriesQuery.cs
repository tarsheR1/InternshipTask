using EventManagementService.Application.DTO;
using EventManagementService.Domain.Pagination;
using MediatR;

namespace EventManagementService.Application.UseCases.Queries.Categories
{
    public record GetPopularCategoriesQuery(
    int MinEventsCount = 5,
    int PageNumber = 1,
    int PageSize = 10) : IRequest<PaginationResponse<CategoryDto>>;
}
