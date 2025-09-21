using EventManagementService.Domain.Entities;
using EventManagementService.Domain.Specification.Base;
using System.Linq.Expressions;

namespace EventManagementService.Domain.Specification.Events
{
    public class UpcomingEventsSpecification : Specification<EventEntity>
    {
        private readonly DateTime _fromDate;

        public UpcomingEventsSpecification(DateTime fromDate)
        {
            _fromDate = fromDate;
        }

        public override Expression<Func<EventEntity, bool>> ToExpression()
        {
            return e => e.Date >= _fromDate;
        }

        public override bool IsSatisfiedBy(EventEntity entity)
        {
            return entity != null && entity.Date >= _fromDate;
        }
    }
}
