using EventManagementService.Domain.Entities;
using EventManagementService.Domain.Interfaces.Repositories;
using EventManagementService.Domain.Interfaces.Specification;
using EventManagementService.Domain.Pagination;
using EventManagementService.Domain.Specification.Categories;
using Microsoft.EntityFrameworkCore;

namespace EventManagementService.Infrastructure.DataAccess.Repositories
{
    public class CategoryRepository : BaseRepository<CategoryEntity, Guid>, ICategoryRepository
    {
        public CategoryRepository(DbContext context) : base(context)
        {
        }
    }
}
