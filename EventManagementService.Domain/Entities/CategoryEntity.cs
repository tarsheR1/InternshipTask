namespace EventManagementService.Domain.Models
{
    public class CategoryEntity
    {
        public Guid Id;
        public string Title;
     
        public List<EventEntity> Events { get; set; } 
    }
}
