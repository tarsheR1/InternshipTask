namespace EventManagementService.Application.DTO.Events
{
    public record CategoryDto
    {
        public int Id;
        public string Title;

        public List<EventDto> Events;
    }
}
