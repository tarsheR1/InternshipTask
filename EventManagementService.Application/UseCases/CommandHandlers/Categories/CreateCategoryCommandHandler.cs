using EventManagementService.Application.UseCases.Сommands.Categories;
using EventManagementService.Domain.Entities;
using EventManagementService.Domain.Interfaces.Repositories;
using MediatR;

namespace EventManagementService.Application.UseCases.CommandHandlers.Categories
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Guid>
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(
            ICategoryRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(
            CreateCategoryCommand request,
            CancellationToken ct)
        {
            var category = _mapper.Map<CategoryEntity>(request);
            await _repository.AddAsync(category, ct);
            return category.Id;
        }
    }
}
