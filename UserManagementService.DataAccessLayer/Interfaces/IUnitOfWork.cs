using UserManagementService.DataAccessLayer.Interfaces.Repositories.Relations;
using UserManagementService.DataAccessLayer.Interfaces.Repositories.Roles;
using UserManagementService.DataAccessLayer.Interfaces.Repositories.Users;

namespace UserManagementService.DataAccessLayer.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IRoleRepository Roles { get; }
        IUserRoleRepository UserRoles { get; }
        IRolePermissionRepository RolePermission { get; }
        IPermissionRepository Permissions { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task CommitTransactionAsync(CancellationToken cancellationToken = default);
        Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
    }
}
