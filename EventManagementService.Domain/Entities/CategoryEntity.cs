namespace EventManagementService.Domain.Entities
{
    public class CategoryEntity : BaseEntity<Guid>
    {
        public string Title { get; set; }

        public ICollection<EventEntity> Events { get; set; } 
    }
}
