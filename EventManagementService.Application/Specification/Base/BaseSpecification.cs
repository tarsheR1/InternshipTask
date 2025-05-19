using EventManagementService.Domain.Interfaces.Specification;
using System.Linq.Expressions;

namespace EventManagementService.Application.Specification.Base
{
    public abstract class BaseSpecification<T> : ISpecification<T>
    {
        public Expression<Func<T, bool>>? Criteria { get; }
        public List<Expression<Func<T, object>>> Includes { get; } = new();
        public Expression<Func<T, object>>? OrderBy { get; private set; }
        public Expression<Func<T, object>>? OrderByDescending { get; private set; }

        public int Skip { get; private set; }
        public int Take { get; private set; }
        public bool IsPagingEnabled { get; private set; }

        protected BaseSpecification(Expression<Func<T, bool>>? criteria = null)
        {
            Criteria = criteria;
        }

        protected void AddInclude(Expression<Func<T, object>> include)
            => Includes.Add(include);

        protected void AddOrderBy(Expression<Func<T, object>> orderBy)
            => OrderBy = orderBy;

        protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescending)
            => OrderByDescending = orderByDescending;

        protected void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }
    }
}
