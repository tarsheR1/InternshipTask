using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagementService.DataAccessLayer.Entities;

namespace UserManagementService.DataAccessLayer.Repositories.Interfaces
{
    public interface IUserRoleRepository
    {
        Task<UserRoleEntity> GetAsync(Guid userId, int roleId);
        Task<IEnumerable<UserRoleEntity>> GetAllAsync();
        Task AddAsync(UserRoleEntity userRole);
        Task DeleteAsync(Guid userId, int roleId);
        Task<bool> ExistsAsync(Guid userId, int roleId);
                                                                                
        Task<IEnumerable<RoleEntity>> GetRolesForUserAsync(Guid userId);
        Task<IEnumerable<UserEntity>> GetUsersForRoleAsync(int roleId);
        Task AddRoleToUserAsync(Guid userId, int roleId);
        Task RemoveRoleFromUserAsync(Guid userId, int roleId);
        Task UpdateUserRolesAsync(Guid userId, IEnumerable<int> roleIds);
    }
}