using EventManagementService.Domain.Entities;

namespace EventManagementService.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryEntity>> GetAllAsync();

        Task<CategoryEntity> GetByIdAsync(int id);

        Task AddAsync(CategoryEntity category);

        Task UpdateAsync(CategoryEntity category);

        Task DeleteAsync(int id);

        Task<bool> ExistsAsync(int id);

        Task<CategoryEntity> GetWithEventsAsync(int id);
    }
}
