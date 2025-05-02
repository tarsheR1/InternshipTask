using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagementService.BusinessLogicLayer.Interfaces.Users;
using UserManagementService.BusinessLogicLayer.Models.Entities.Roles;

[ApiController]
[Route("api/roles/{roleId}/permissions")]
public class RolePermissionsController : ControllerBase
{
    private readonly IRolePermissionAssignmentService _rolePermissionService;

    public RolePermissionsController(IRolePermissionAssignmentService rolePermissionService)
    {
        _rolePermissionService = rolePermissionService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Permission>>> GetRolePermissions(
        [FromRoute] int roleId,
        CancellationToken cancellationToken)
    {
        var permissions = await _rolePermissionService.GetRolePermissionsAsync(roleId, cancellationToken);
        
        return Ok(permissions);
    }

    [HttpPost("{permissionId}")]
    public async Task<IActionResult> AssignPermissionToRole(
        [FromRoute] int roleId,
        [FromRoute] int permissionId,
        CancellationToken cancellationToken)
    {
        await _rolePermissionService.AssignPermissionToRoleAsync(roleId, permissionId, cancellationToken);
       
        return NoContent();
    }

    [HttpDelete("{permissionId}")]
    public async Task<IActionResult> RemovePermissionFromRole(
        [FromRoute] int roleId,
        [FromRoute] int permissionId,
        CancellationToken cancellationToken)
    {
        await _rolePermissionService.RemovePermissionFromRoleAsync(roleId, permissionId, cancellationToken);
       
        return NoContent();
    }
}