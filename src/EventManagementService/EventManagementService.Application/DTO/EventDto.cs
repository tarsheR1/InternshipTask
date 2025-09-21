namespace EventManagementService.Application.DTO
{
    public record EventDto
    {
        public Guid Id;
        public string Title;
        public string? Description;
        public DateTime Date;
        public string Location;
        public int CategoryId;
        public bool IsActive;

        public ICollection<CategoryDto> Categories;
    }
}
