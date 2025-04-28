using Microsoft.EntityFrameworkCore;
using UserManagementService.DataAccessLayer.Entities.Role;
using UserManagementService.DataAccessLayer.Repositories.Base;
using UserManagementService.DataAccessLayer.Persistence;
using UserManagementService.DataAccessLayer.Interfaces.Repositories.Roles;

namespace UserManagementService.DataAccessLayer.Repositories.Roles
{
    public class RoleRepository : BaseRepository<RoleEntity, int>, IRoleRepository
    {
        private readonly UserManagementDbContext _context;

        public RoleRepository(UserManagementDbContext context) : base(context) { }

        public async Task<List<RoleEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Roles.ToListAsync(cancellationToken);
        }

        public async Task<RoleEntity> GetByNameAsync(string roleName, CancellationToken cancellationToken)
        {
            return await _context.Roles
                .Include(r => r.RolePermissions)
                .FirstOrDefaultAsync(r => r.Name == roleName, cancellationToken);
        }
    }
}
