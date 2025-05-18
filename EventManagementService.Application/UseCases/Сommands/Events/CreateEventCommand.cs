using MediatR;

namespace EventManagementService.Application.UseCases.Сommands.Events
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
