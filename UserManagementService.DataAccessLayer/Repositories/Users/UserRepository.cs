using Microsoft.EntityFrameworkCore;
using UserManagementService.DataAccessLayer.Entities.Role;
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
                .Include(u => u.Roles)
                .ThenInclude(r => r.Permissions)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<List<RoleEntity>> GetUserRolesAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await _context.UserRoles
                .Where(ur => ur.UserId == userId)
                .Include(ur => ur.Role)
                .Select(ur => ur.Role)
                .ToListAsync(cancellationToken);
        }
    }
}
