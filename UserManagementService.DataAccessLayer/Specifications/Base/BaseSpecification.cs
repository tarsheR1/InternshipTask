using System.Linq.Expressions;
using Shared.Interfaces;

namespace UserManagementService.DataAccessLayer.Specifications.Base
{
    public abstract class BaseSpecification<T> : ISpecification<T>
    {
        public Expression<Func<T, bool>> Criteria { get; }
        public List<Expression<Func<T, object>>> Includes { get; } = new();
        public Expression<Func<T, object>> OrderBy { get; private set; }
        public Expression<Func<T, object>> OrderByDescending { get; private set; }

        protected BaseSpecification(Expression<Func<T, bool>> criteria)
            => Criteria = criteria;

        protected void AddInclude(Expression<Func<T, object>> include)
            => Includes.Add(include);

        protected void AddOrderBy(Expression<Func<T, object>> orderBy)
            => OrderBy = orderBy;

        protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescending)
            => OrderByDescending = orderByDescending;

        public void ApplyOrdering(string sortBy, bool isDescending)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, sortBy);
            var lambda = Expression.Lambda<Func<T, object>>(property, parameter);

            if (isDescending)
                AddOrderByDescending(lambda);
            else
                AddOrderBy(lambda);
        }
    }
}
