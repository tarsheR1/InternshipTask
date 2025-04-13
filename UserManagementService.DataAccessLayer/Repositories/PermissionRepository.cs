using Microsoft.EntityFrameworkCore;
using UserManagementService.DataAccessLayer.Entities;
using UserManagementService.DataAccessLayer.Interfaces.Repositories;
using UserManagementService.DataAccessLayer.Persistence;

namespace UserManagementService.DataAccessLayer.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly UserManagementDbContext _context;

        public PermissionRepository(UserManagementDbContext context)
        {
            _context = context;
        }

        public async Task<List<PermissionEntity>> GetAll()
        {
            return await _context.Permissions
                .AsNoTracking() 
                .ToListAsync();
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
    }
}
