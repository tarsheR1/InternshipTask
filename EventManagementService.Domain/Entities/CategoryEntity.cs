using EventManagementService.Domain.Entities;

namespace EventManagementService.Domain.Models
{
    public class CategoryEntity : BaseEntity<Guid>
    {
        public string Title { get; set; }

        public ICollection<EventEntity> Events { get; set; } 
    }
}
