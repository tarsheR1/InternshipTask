namespace EventManagementService.Domain.Models
{
    public class CategoryEntity
    {
        public int Id;
        public string Title;
     
        public List<EventEntity> Events { get; set; } 
    }
}
