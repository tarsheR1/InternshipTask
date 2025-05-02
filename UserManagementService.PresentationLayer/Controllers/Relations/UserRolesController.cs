using Microsoft.AspNetCore.Mvc;
using UserManagementService.BusinessLogicLayer.Interfaces.Users;
using UserManagementService.BusinessLogicLayer.Models.Entities.Roles;

namespace UserManagementService.PresentationLayer.Controllers.Relations
{
    [ApiController]
    [Route("api/users/{userId}/roles")]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleService _userRoleService;

        public UserRoleController(IUserRoleService userRoleService)
        {
            _userRoleService = userRoleService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Role>>> GetUserRoles(
            [FromRoute] Guid userId,
            CancellationToken cancellationToken)
        {
            return await _userRoleService.GetUserRolesAsync(userId, cancellationToken);
        }

        [HttpPost("{roleId}")]
        public async Task<IActionResult> AssignRole(
            [FromRoute] Guid userId,
            [FromRoute] int roleId,
            CancellationToken cancellationToken)
        {
            await _userRoleService.AssignRoleToUserAsync(userId, roleId, cancellationToken);
            
            return NoContent();
        }

        [HttpDelete("{roleId}")]
        public async Task<IActionResult> RemoveRole(
            [FromRoute] Guid userId,
            [FromRoute] int roleId,
            CancellationToken cancellationToken)
        {
            await _userRoleService.RemoveRoleFromUserAsync(userId, roleId, cancellationToken);
           
            return NoContent();
        }
    }
}