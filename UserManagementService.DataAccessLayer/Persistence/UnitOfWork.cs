using UserManagementService.DataAccessLayer.Interfaces;
using UserManagementService.DataAccessLayer.Interfaces.Repositories.Roles;
using UserManagementService.DataAccessLayer.Interfaces.Repositories.Users;

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
        IRefreshTokenRepository refreshTokenRepository)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            Permissions = permissionRepository ?? throw new ArgumentNullException(nameof(permissionRepository));
            Users = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            Roles = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            RefreshTokens = refreshTokenRepository ?? throw new ArgumentNullException(nameof(refreshTokenRepository));
        }

        public IPermissionRepository Permissions { get; }
        public IUserRepository Users { get; }
        public IRoleRepository Roles { get; }

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
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }
    }
}
