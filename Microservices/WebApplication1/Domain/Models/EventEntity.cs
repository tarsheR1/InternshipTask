namespace EventManagementService.Domain.Models
{
    public class EventEntity
    {
        public Guid Id;
        public string Title;
        public string Description;
        public DateTime Date;
        public string Location;
        public int CategoryId;

        public CategoryEntity Category;
    }
}
