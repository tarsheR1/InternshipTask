using EventManagementService.Application.UseCases.Сommands.Categories;
using EventManagementService.Domain.Interfaces.Repositories;
using MediatR;

namespace EventManagementService.Application.UseCases.CommandHandlers.Categories
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
    {
        private readonly ICategoryRepository _repository;

        public DeleteCategoryCommandHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(
            DeleteCategoryCommand request,
            CancellationToken ct)
        {
            var category = await _repository.GetByIdAsync(request.Id, ct)
                ?? throw new KeyNotFoundException(request.Id.ToString());

            await _repository.DeleteAsync(category, ct);
        }
    }
}
