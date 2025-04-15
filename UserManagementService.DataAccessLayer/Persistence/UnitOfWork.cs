using UserManagementService.DataAccessLayer.Interfaces;
using UserManagementService.DataAccessLayer.Interfaces.Repositories;
using UserManagementService.DataAccessLayer.Repositories;
using UserManagementService.DataAccessLayer.Repositories.Interfaces;

namespace UserManagementService.DataAccessLayer.Persistence
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly UserManagementDbContext _context;
        private bool _disposed;

        public UnitOfWork(UserManagementDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUserRepository Users => new UserRepository(_context);
        public IRoleRepository Roles => new RoleRepository(_context);
        public IUserRoleRepository UserRoles => new UserRoleRepository(_context);
        public IRolePermissionRepository RolePermission => new RolePermissionsRepository(_context);
        public IRefreshTokenRepository RefreshTokens => new RefreshTokenRepository(_context);

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
