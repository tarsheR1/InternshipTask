using Microsoft.EntityFrameworkCore;
using UserManagementService.DataAccessLayer.Entities;
using UserManagementService.DataAccessLayer.Persistence;
using UserManagementService.DataAccessLayer.Repositories.Interfaces;

namespace UserManagementService.DataAccessLayer.Repositories
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly UserManagementDbContext _context;

        public UserRoleRepository(UserManagementDbContext context)
        {
            _context = context;
        }

        public async Task<UserRoleEntity> GetAsync(Guid userId, int roleId)
        {
            return await _context.UserRoles
                .Include(ur => ur.User)
                .Include(ur => ur.Role)
                .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId);
        }

        public async Task<IEnumerable<UserRoleEntity>> GetAllAsync()
        {
            return await _context.UserRoles
                .Include(ur => ur.User)
                .Include(ur => ur.Role)
                .ToListAsync();
        }

        public async Task AddAsync(UserRoleEntity userRole)
        {
            await _context.UserRoles.AddAsync(userRole);
        }

        public async Task DeleteAsync(Guid userId, int roleId)
        {
            var entity = await GetAsync(userId, roleId);
            if (entity != null)
            {
                _context.UserRoles.Remove(entity);
            }
        }

        public async Task<bool> ExistsAsync(Guid userId, int roleId)
        {
            return await _context.UserRoles
                .AnyAsync(ur => ur.UserId == userId && ur.RoleId == roleId);
        }

        public async Task<IEnumerable<RoleEntity>> GetRolesForUserAsync(Guid userId)
        {
            return await _context.UserRoles
                .Where(ur => ur.UserId == userId)
                .Include(ur => ur.Role)
                .Select(ur => ur.Role)
                .ToListAsync();
        }

        public async Task<IEnumerable<UserEntity>> GetUsersForRoleAsync(int roleId)
        {
            return await _context.UserRoles
                .Where(ur => ur.RoleId == roleId)
                .Include(ur => ur.User)
                .Select(ur => ur.User)
                .ToListAsync();
        }

        public async Task AddRoleToUserAsync(Guid userId, int roleId)
        {
            if (!await ExistsAsync(userId, roleId))
            {
                await AddAsync(new UserRoleEntity
                {
                    UserId = userId,
                    RoleId = roleId
                });
            }
        }

        public async Task RemoveRoleFromUserAsync(Guid userId, int roleId)
        {
            await DeleteAsync(userId, roleId);
        }

        public async Task UpdateUserRolesAsync(Guid userId, IEnumerable<int> roleIds)
        {
            var currentRoles = await _context.UserRoles
                .Where(ur => ur.UserId == userId)
                .ToListAsync();

            var rolesToRemove = currentRoles
                .Where(cr => !roleIds.Contains(cr.RoleId))
                .ToList();

            _context.UserRoles.RemoveRange(rolesToRemove);

            var existingRoleIds = currentRoles.Select(cr => cr.RoleId);
            var rolesToAdd = roleIds
                .Where(rid => !existingRoleIds.Contains(rid))
                .Select(rid => new UserRoleEntity
                {
                    UserId = userId,
                    RoleId = rid
                });

            await _context.UserRoles.AddRangeAsync(rolesToAdd);
        }

        public async Task<bool> UserHasRoleAsync(Guid userId, int roleId)
        {
            return await ExistsAsync(userId, roleId);
        }

        public async Task<bool> UserHasAnyRoleAsync(Guid userId, IEnumerable<int> roleIds)
        {
            return await _context.UserRoles
                .AnyAsync(ur => ur.UserId == userId && roleIds.Contains(ur.RoleId));
        }
    }
}
