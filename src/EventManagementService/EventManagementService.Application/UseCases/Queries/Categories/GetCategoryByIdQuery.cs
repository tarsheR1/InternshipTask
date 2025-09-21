using EventManagementService.Application.DTO;
using MediatR;

namespace EventManagementService.Application.UseCases.Queries.Categories
{
    public record GetCategoryByIdQuery(Guid Id) : IRequest<CategoryDto>;
}
