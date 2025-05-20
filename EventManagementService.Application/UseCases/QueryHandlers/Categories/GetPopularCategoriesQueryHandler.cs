using EventManagementService.Application.DTO;
using EventManagementService.Application.UseCases.Queries.Categories;
using EventManagementService.Domain.Interfaces.Repositories;
using EventManagementService.Domain.Pagination;
using EventManagementService.Domain.Specification.Categories;
using MediatR;

namespace EventManagementService.Application.UseCases.QueryHandlers.Categories
{
    public class GetPopularCategoriesQueryHandler
    : IRequestHandler<GetPopularCategoriesQuery, PaginationResponse<CategoryDto>>
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public GetPopularCategoriesQueryHandler(
            ICategoryRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PaginationResponse<CategoryDto>> Handle(
            GetPopularCategoriesQuery request,
            CancellationToken ct)
        {
            var pagination = new PaginationOptions(request.PageNumber, request.PageSize);
            var spec = new PopularCategoriesSpecification(request.MinEventsCount);

            var result = await _repository.GetListBySpecAsync(spec, pagination, ct);
            return _mapper.Map<PaginationResponse<CategoryDto>>(result);
        }
    }
}
