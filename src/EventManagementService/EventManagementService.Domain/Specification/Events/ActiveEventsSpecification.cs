using EventManagementService.Domain.Entities;
using EventManagementService.Domain.Specification.Base;
using System.Linq.Expressions;

namespace EventManagementService.Domain.Specification.Events
{
    public class ActiveEventsSpecification : Specification<EventEntity>
    {
        public override Expression<Func<EventEntity, bool>> ToExpression()
        {
            return e => e.IsActive;
        }

        public override bool IsSatisfiedBy(EventEntity entity)
        {
            return entity != null && entity.IsActive;
        }

    }
}
