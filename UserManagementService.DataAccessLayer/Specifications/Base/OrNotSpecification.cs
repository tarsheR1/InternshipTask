using Shared.Interfaces;

namespace UserManagementService.DataAccessLayer.Specifications.Base
{
    public class OrNotSpecification<T> : Specification<T>
    {
        private readonly ISpecification<T> _left;
        private readonly ISpecification<T> _right;

        public OrNotSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            _left = left;
            _right = right;

            OrderBy = left.OrderBy;
            OrderByDescending = left.OrderByDescending;
            Skip = left.Skip;
            Take = left.Take;
        }

        public override bool IsSatisfiedBy(T candidate)
            => _left.IsSatisfiedBy(candidate) || !_right.IsSatisfiedBy(candidate);
    }
}
