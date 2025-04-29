using UserManagementService.DataAccessLayer.Entities.Users;
using UserManagementService.DataAccessLayer.Specifications.Base;

namespace UserManagementService.DataAccessLayer.Specifications.Users
{
    public class UserByIdSpecification : BaseSpecification<UserEntity>
    {
        public UserByIdSpecification(Guid userId)
            : base(u => u.Id == userId)
        {
            AddInclude(u => u.UserRoles);
            AddInclude(u => u.UserRoles.Select(ur => ur.Role));
            AddInclude(u => u.UserRoles.Select(ur => ur.Role.RolePermissions));
            AddInclude(u => u.UserRoles.Select(ur => ur.Role.RolePermissions.Select(rp => rp.Permission)));
        }
    }
}
