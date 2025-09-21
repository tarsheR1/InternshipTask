using EventManagementService.Domain.Interfaces.Specification;
using EventManagementService.Domain.Specification.Composite;
using System.Linq.Expressions;

namespace EventManagementService.Domain.Specification.Base
{
    public abstract class Specification<T> : ISpecification<T>
    {
        public abstract bool IsSatisfiedBy(T entity);

        public abstract Expression<Func<T, bool>> ToExpression();

        public ISpecification<T> And(ISpecification<T> other)
            => new AndSpecification<T>(this, other);

        public ISpecification<T> AndNot(ISpecification<T>other)
            => new AndNotSpecification<T>(this, other);

        public ISpecification<T> Or(ISpecification<T> other)
            => new OrSpecification<T>(this, other);

        public ISpecification<T> OrNot(ISpecification<T> other)
            => new OrNotSpecification<T>(this, other);

        public ISpecification<T> Not()
            => new NotSpecification<T>(this);
    }
}
