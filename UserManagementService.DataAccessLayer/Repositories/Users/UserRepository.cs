using Microsoft.EntityFrameworkCore;
using UserManagementService.DataAccessLayer.Entities.Users;
using UserManagementService.DataAccessLayer.Interfaces.Repositories.Users;
using UserManagementService.DataAccessLayer.Persistence;
using UserManagementService.DataAccessLayer.Repositories.Base;

namespace UserManagementService.DataAccessLayer.Repositories.Users
{
    public class UserRepository : BaseRepository<UserEntity, Guid>, IUserRepository
    {
        private readonly UserManagementDbContext _context;

        public UserRepository(UserManagementDbContext context) : base(context) { }

        public async Task<UserEntity> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .ThenInclude(rp => rp.RolePermissions)
                .ThenInclude(p => p.Permission)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<List<string>> GetUserRolesAsync(Guid userId, CancellationToken cancellationToken)
        {
            var roleNames = await _context.Users
                .Where(u => u.Id == userId)
                .SelectMany(u => u.UserRoles.Select(ur => ur.Role.Name))
                .ToListAsync(cancellationToken);

            return roleNames;
        }
    }
}
