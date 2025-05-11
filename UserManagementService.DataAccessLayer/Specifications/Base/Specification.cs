using Shared.Interfaces;
using UserManagementService.DataAccessLayer.Specifications.Composite;

namespace UserManagementService.DataAccessLayer.Specifications.Base
{
    public abstract class Specification<T> : ISpecification<T>
    {
        public abstract bool IsSatisfiedBy(T entity);

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
