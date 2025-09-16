namespace EventManagementService.Domain.Entities
{
    public class EventEntity : BaseEntity <Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<CategoryEntity> Categories { get; set; }
    }
}
