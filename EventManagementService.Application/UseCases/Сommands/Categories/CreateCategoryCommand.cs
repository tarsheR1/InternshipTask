using MediatR;

namespace EventManagementService.Application.UseCases.Сommands.Categories
{
    public record CreateCategoryCommand(
    string Title,
    string? Description = null) : IRequest<Guid>;
}
