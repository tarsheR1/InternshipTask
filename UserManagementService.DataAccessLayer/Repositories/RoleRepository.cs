using Microsoft.EntityFrameworkCore;
using UserManagementService.DataAccessLayer.Entities;
using UserManagementService.DataAccessLayer.Interfaces.Repositories;
using UserManagementService.DataAccessLayer.Persistence;

namespace UserManagementService.DataAccessLayer.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly UserManagementDbContext _context;

        public RoleRepository(UserManagementDbContext context)
        {
            _context = context;
        }

        public async Task<List<RoleEntity>> GetAll()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<RoleEntity> GetByIdAsync(int roleId, CancellationToken cancellationToken)
        {
            return await _context.Roles
                .Include(r => r.RolePermissions)
                .FirstOrDefaultAsync(r => r.Id == roleId, cancellationToken);
        }

        public async Task<RoleEntity> GetByName(string roleName, CancellationToken cancellationToken)
        {
            return await _context.Roles
                .Include(r => r.RolePermissions)
                .FirstOrDefaultAsync(r => r.Name == roleName, cancellationToken);
        }

        public async Task AddAsync(RoleEntity role, CancellationToken cancellationToken)
        {
            await _context.Roles.AddAsync(role, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(RoleEntity role, CancellationToken cancellationToken)
        {
            _context.Roles.Update(role);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(RoleEntity role, CancellationToken cancellationToken)
        {
            _context.Roles.Remove(role);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
