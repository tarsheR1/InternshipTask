using UserManagementService.DataAccessLayer.Entities.Users;
using UserManagementService.DataAccessLayer.Specifications.Base;

namespace UserManagementService.DataAccessLayer.Specifications.Users
{
        public class UserSpecification : BaseSpecification<UserEntity>
        {
            public UserSpecification(UserFilter filter)
                : base(u =>
                    (filter == null || string.IsNullOrEmpty(filter.Search) ||
                     u.Email.Contains(filter.Search) ||
                     u.FirstName.Contains(filter.Search) ||
                     u.LastName.Contains(filter.Search)) &&
                    (!filter.IsActive.HasValue || u.IsActive == filter.IsActive))
            {
                AddInclude(u => u.UserRoles);
            }
        }
}
