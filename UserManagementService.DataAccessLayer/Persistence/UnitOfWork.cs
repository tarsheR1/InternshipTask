using UserManagementService.DataAccessLayer.Interfaces;
using UserManagementService.DataAccessLayer.Interfaces.Repositories;

namespace UserManagementService.DataAccessLayer.Persistence
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly UserManagementDbContext _context;
        private bool _disposed;

        public UnitOfWork(
        UserManagementDbContext context,
        IPermissionRepository permissionRepository,
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        IUserRoleRepository userRoleRepository,
        IRolePermissionRepository rolePermissionRepository,
        IRefreshTokenRepository refreshTokenRepository)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            Permissions = permissionRepository ?? throw new ArgumentNullException(nameof(permissionRepository));
            Users = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            Roles = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            UserRoles = userRoleRepository ?? throw new ArgumentNullException(nameof(userRoleRepository));
            RolePermission = rolePermissionRepository ?? throw new ArgumentNullException(nameof(rolePermissionRepository));
            RefreshTokens = refreshTokenRepository ?? throw new ArgumentNullException(nameof(refreshTokenRepository));
        }

        public IPermissionRepository Permissions { get; }
        public IUserRepository Users { get; }
        public IRoleRepository Roles { get; }
        public IUserRoleRepository UserRoles { get; }
        public IRolePermissionRepository RolePermission { get; }
        public IRefreshTokenRepository RefreshTokens { get; }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            await _context.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            await _context.Database.CommitTransactionAsync(cancellationToken);
        }

        public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
        {
            await _context.Database.RollbackTransactionAsync(cancellationToken);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

}
