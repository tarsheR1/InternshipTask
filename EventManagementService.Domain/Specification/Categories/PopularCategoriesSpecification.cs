using EventManagementService.Domain.Entities;
using EventManagementService.Domain.Specification.Base;
using System.Linq.Expressions;

namespace EventManagementService.Domain.Specification.Categories
{
    public class PopularCategoriesSpecification : Specification<CategoryEntity>
    {
        private readonly int _minEventsCount;

        public PopularCategoriesSpecification(int minEventsCount)
        {
            _minEventsCount = minEventsCount;
        }

        public override Expression<Func<CategoryEntity, bool>> ToExpression()
        {
            return c => c.Events.Count >= _minEventsCount;
        }

        public override bool IsSatisfiedBy(CategoryEntity category)
        {
            return category != null &&
                   category.Events != null &&
                   category.Events.Count >= _minEventsCount;
        }
    }

}
