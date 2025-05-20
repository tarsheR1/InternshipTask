using EventManagementService.Application.DTO;
using EventManagementService.Application.UseCases.Queries.Categories;
using EventManagementService.Domain.Interfaces.Repositories;
using MediatR;

namespace EventManagementService.Application.UseCases.QueryHandlers.Categories
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDto>
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public GetCategoryByIdQueryHandler(
            ICategoryRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CategoryDto> Handle(
            GetCategoryByIdQuery request,
            CancellationToken ct)
        {
            var category = await _repository.GetByIdAsync(request.Id, ct)
                ?? throw new KeyNotFoundException(request.Id.ToString());

            return _mapper.Map<CategoryDto>(category);
        }
    }
}
