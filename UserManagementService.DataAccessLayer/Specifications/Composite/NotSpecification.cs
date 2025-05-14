using Shared.Interfaces;
using System.Linq.Expressions;
using UserManagementService.DataAccessLayer.Specifications.Base;

namespace UserManagementService.DataAccessLayer.Specifications.Composite
{
    public class NotSpecification<T> : Specification<T>
    {
        private readonly ISpecification<T> _spec;

        public NotSpecification(ISpecification<T> specification)
        {
            _spec = specification;
        }

        public override bool IsSatisfiedBy(T candidate)
            => !_spec.IsSatisfiedBy(candidate);

        public override Expression<Func<T, bool>> ToExpression()
        {
            var expression = _spec.ToExpression();
            var notExpression = Expression.Not(expression.Body);

            return Expression.Lambda<Func<T, bool>>(
                notExpression,
                expression.Parameters.Single());
        }
    }
}
