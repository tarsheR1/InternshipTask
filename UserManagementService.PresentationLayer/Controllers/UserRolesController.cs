using Microsoft.AspNetCore.Mvc;
using UserManagementService.BusinessLogicLayer.Interfaces.Users;
using UserManagementService.BusinessLogicLayer.Models.Entities.Roles;

namespace UserManagementService.Presentation.Controllers
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
        public async Task<ActionResult<List<Role>>> Get(Guid userId, CancellationToken cancellationToken)
        {
            return await _userRoleService.GetUserRolesAsync(userId, cancellationToken);
        }

        [HttpPost("{roleId}")]
        public async Task<IActionResult> Post(Guid userId, int roleId, CancellationToken cancellationToken)
        {
            await _userRoleService.AssignRoleToUserAsync(userId, roleId, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{roleId}")]
        public async Task<IActionResult> Delete(Guid userId, int roleId, CancellationToken cancellationToken)
        {
            await _userRoleService.RemoveRoleFromUserAsync(userId, roleId, cancellationToken);
            return NoContent();
        }
    }
}