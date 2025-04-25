using EventManagementService.Application.Specification.Base;
using EventManagementService.Domain.Models;

namespace EventManagementService.Application.Specification.Events
{
    public class EventsPagedSpecification : BaseSpecification<EventEntity>
    {
        public EventsPagedSpecification(
            int pageNumber,
            int pageSize,
            int? categoryId = null
        ) : base(e => !categoryId.HasValue || e.CategoryId == categoryId)
        {
            AddOrderBy(e => e.Date);

            ApplyPaging(
                skip: (pageNumber - 1) * pageSize,
                take: pageSize
            );
        }
    }
}
