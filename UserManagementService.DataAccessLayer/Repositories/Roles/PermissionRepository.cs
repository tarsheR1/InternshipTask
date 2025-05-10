using Microsoft.EntityFrameworkCore;
using UserManagementService.DataAccessLayer.Entities.Role;
using UserManagementService.DataAccessLayer.Interfaces.Repositories.Roles;
using UserManagementService.DataAccessLayer.Persistence;

namespace UserManagementService.DataAccessLayer.Repositories.Roles
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly UserManagementDbContext _context;

        public PermissionRepository(UserManagementDbContext context)
        {
            _context = context;
        }

        public async Task<List<PermissionEntity>> GetAll(CancellationToken cancellationToken)
        {
            return await _context.Permissions
                .AsNoTracking() 
                .ToListAsync(cancellationToken);
        }

        public async Task<PermissionEntity> GetByIdAsync(int permissionId, CancellationToken cancellationToken)
        {
            return await _context.Permissions
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == permissionId, cancellationToken);
        }

        public async Task<PermissionEntity> GetByName(string permissionName, CancellationToken cancellationToken)
        {
            return await _context.Permissions
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Name == permissionName, cancellationToken);
        }

        public async Task<List<RoleEntity>> GetRolesForPermissionAsync(int permissionId, CancellationToken cancellationToken)
        {
            return await _context.Roles
                .Where(p => p.Id == permissionId)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
    }
}
