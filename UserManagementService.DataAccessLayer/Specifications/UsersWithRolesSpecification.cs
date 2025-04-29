using UserManagementService.BusinessLogicLayer.Models.Entities.Users;

namespace UserManagementService.DataAccessLayer.Specifications
{
    public class UsersWithRolesSpecification : BaseSpecification<User>
    {
        public UsersWithRolesSpecification(bool includeInactive = false)
            : base(includeInactive ? null : u => u.IsActive)
        {
            AddInclude(u => u.UserRoles);
            AddOrderByDescending(u => u.CreatedAt);
        }
    }
}
