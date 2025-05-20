namespace EventManagementService.Application.DTO
{
    public record CategoryDto
    {
        public int Id;
        public string Title;

        public List<EventDto> Events;
    }
}
