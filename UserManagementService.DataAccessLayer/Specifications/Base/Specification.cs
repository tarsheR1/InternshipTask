using System.Linq.Expressions;
using Shared.Interfaces;

namespace UserManagementService.DataAccessLayer.Specifications.Base
{
    public abstract class Specification<T> : ISpecification<T>
    {
        public Expression<Func<T, bool>> Expression { get; }

        public bool IsSatisfiedBy(T entity)
        {
            Func<T, bool> predicate = ToExpression().Compile();
            return predicate(entity);
        }

        public Expression<Func<T, object>>? OrderBy { get; protected set; }
        public Expression<Func<T, object>>? OrderByDescending { get; protected set; }
        public int? Skip { get; protected set; }
        public int? Take { get; protected set; }

        public abstract Expression<Func<T, bool>> ToExpression();

        protected void AddOrderBy(Expression<Func<T, object>> orderBy)
            => OrderBy = orderBy;

        protected void AddOrderByDescending(Expression<Func<T, object>> orderByDesc)
            => OrderByDescending = orderByDesc;

        protected void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
        }

        public ISpecification<T> And(ISpecification<T> other)
            => new AndSpecification<T>(this, other);

        public ISpecification<T> AndNot(ISpecification<T>    other)
            => new AndNotSpecification<T>(this, other);

        public ISpecification<T> Or(ISpecification<T> other)
            => new OrSpecification<T>(this, other);

        public ISpecification<T> OrNot(ISpecification<T> other)
            => new OrNotSpecification<T>(this, other);

        public ISpecification<T> Not()
            => new NotSpecification<T>(this);
    }

}
