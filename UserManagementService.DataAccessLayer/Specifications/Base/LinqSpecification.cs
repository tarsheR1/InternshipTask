using System.Linq.Expressions;

namespace UserManagementService.DataAccessLayer.Specifications.Base
{
    public abstract class LinqSpecification<T> : Specification<T>
    {
        public abstract Expression<Func<T, bool>> AsExpression();
        public override bool IsSatisfiedBy(T candidate) => AsExpression().Compile()(candidate);
    }
}