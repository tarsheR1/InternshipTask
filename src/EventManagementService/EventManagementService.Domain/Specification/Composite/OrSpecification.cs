using EventManagementService.Domain.Interfaces.Specification;
using EventManagementService.Domain.Specification.Base;
using System.Linq.Expressions;

namespace EventManagementService.Domain.Specification.Composite
{
    public class OrSpecification<T> : Specification<T>
    {
        private readonly ISpecification<T> _left;
        private readonly ISpecification<T> _right;

        public OrSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            _left = left;
            _right = right;
        }

        public override bool IsSatisfiedBy(T candidate)
            => _left.IsSatisfiedBy(candidate) || _right.IsSatisfiedBy(candidate);

        public override Expression<Func<T, bool>> ToExpression()
        {
            var leftExpression = _left.ToExpression();
            var rightExpression = _right.ToExpression();

            var orExpression = Expression.OrElse(
                leftExpression.Body,
                rightExpression.Body);

            return Expression.Lambda<Func<T, bool>>(
                orExpression,
                leftExpression.Parameters.Single());
        }
    }
}
