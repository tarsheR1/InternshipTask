using EventManagementService.Application.Specification.Base;
using EventManagementService.Domain.Models;

namespace EventManagementService.Application.Specification.Events
{
    public class EventsAfterDateSpecification : BaseSpecification<EventEntity>
    {
        public EventsAfterDateSpecification(DateTime date)
            : base(e => e.Date >= date)
        {
            AddOrderBy(e => e.Date);
        }
    }
}
