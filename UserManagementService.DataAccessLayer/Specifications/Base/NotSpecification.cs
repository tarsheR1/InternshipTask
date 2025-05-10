using Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagementService.DataAccessLayer.Specifications.Base
{
    public class NotSpecification<T> : Specification<T>
    {
        private readonly ISpecification<T> _spec;

        public NotSpecification(ISpecification<T> specification)
        {
            _spec = specification;

            OrderBy = specification.OrderBy;
            OrderByDescending = specification.OrderByDescending;
            Skip = specification.Skip;
            Take = specification.Take;
        }

        public override bool IsSatisfiedBy(T candidate)
            => !_spec.IsSatisfiedBy(candidate);
    }
}
