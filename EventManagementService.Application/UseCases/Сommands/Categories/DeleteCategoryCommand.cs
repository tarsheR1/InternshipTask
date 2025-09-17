using MediatR;

namespace EventManagementService.Application.UseCases.Сommands.Categories
{
    public record DeleteCategoryCommand(Guid Id) : IRequest;
}
