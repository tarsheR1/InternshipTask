using EventManagementService.Domain.Entities;
using MediatR;

namespace EventManagementService.Application.UseCases.Сommands.Events
{
    public class UpdateEventCommand : IRequest<Guid>
    {
        public Guid Id { get; set; } 
        public string Title { get; set; } 
        public string Description { get; set; } 
        public DateTime Date { get; set; } 
        public string Location { get; set; } 

        public ICollection<CategoryEntity> Categories { get; set; } 
    }
}
