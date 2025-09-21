using MediatR;

namespace EventManagementService.Application.UseCases.Сommands.Categories
{
    public record UpdateCategoryCommand(
     Guid Id,
     string Title,
     string? Description = null) : IRequest;

}
