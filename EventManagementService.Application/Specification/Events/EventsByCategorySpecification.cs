using EventManagementService.Application.Specification.Base;
using EventManagementService.Domain.Entities;

namespace EventManagementService.Application.Specification.Events
{
    public class EventsByCategorySpecification : BaseSpecification<EventEntity>
    {
        public EventsByCategorySpecification(int categoryId)
            : base(e => e.CategoryId == categoryId)
        {
            AddInclude(e => e.Category); 
        }
    }
}
