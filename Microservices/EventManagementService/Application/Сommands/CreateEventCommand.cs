using MediatR;

namespace EventManagementService.Application.Сommands
{
    public class CreateEventCommand : IRequest<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public int CategoryId { get; set; }
    }
}
