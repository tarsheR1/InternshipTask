using MediatR;

namespace EventManagementService.Application.Сommands
{
    public class UpdateEventCommand : IRequest<Guid>
    {
        public Guid Id { get; set; } 
        public string Title { get; set; } 
        public string Description { get; set; } 
        public DateTime Date { get; set; } 
        public string Location { get; set; } 
        public int CategoryId { get; set; } 
    }
}
