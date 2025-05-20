using EventManagementService.Application.UseCases.Сommands.Categories;
using EventManagementService.Domain.Entities;
using EventManagementService.Domain.Interfaces.Repositories;
using MediatR;

namespace EventManagementService.Application.UseCases.CommandHandlers.Categories
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand>
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public UpdateCategoryCommandHandler(
            ICategoryRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(
            UpdateCategoryCommand request,
            CancellationToken ct)
        {
            var category = await _repository.GetByIdAsync(request.Id, ct)
                ?? throw new NotFoundException(nameof(CategoryEntity), request.Id);

            _mapper.Map(request, category);
            await _repository.UpdateAsync(category, ct);
        }
    }
}
