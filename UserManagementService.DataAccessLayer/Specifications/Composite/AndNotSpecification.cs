using Shared.Interfaces;
using System.Linq.Expressions;
using UserManagementService.DataAccessLayer.Specifications.Base;

namespace UserManagementService.DataAccessLayer.Specifications.Composite
{
    public class AndNotSpecification<T> : Specification<T>
    {
        private readonly ISpecification<T> _left;
        private readonly ISpecification<T> _right;

        public AndNotSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            _left = left;
            _right = right;
        }

        public override bool IsSatisfiedBy(T candidate)
            => _left.IsSatisfiedBy(candidate) && !_right.IsSatisfiedBy(candidate);

        public override Expression<Func<T, bool>> ToExpression()
        {
            var leftExpression = _left.ToExpression();
            var rightExpression = _right.ToExpression();

            var notRightExpression = Expression.Not(rightExpression.Body);
            var andNotExpression = Expression.AndAlso(
                leftExpression.Body,
                notRightExpression);

            return Expression.Lambda<Func<T, bool>>(
                andNotExpression,
                leftExpression.Parameters.Single());
        }
    }
}
