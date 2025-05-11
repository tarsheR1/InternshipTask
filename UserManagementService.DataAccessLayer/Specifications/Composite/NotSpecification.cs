using Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
