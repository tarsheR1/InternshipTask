using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using UserManagementService.BusinessLogicLayer.Interfaces.Users;
using UserManagementService.BusinessLogicLayer.Models.Entities.Roles;

[ApiController]
[Route("api/roles/{roleId}/permissions")]
public class RolePermissionsController : ControllerBase
{
    private readonly IRolePermissionAssignmentService _rolePermissionService;
    private readonly ILogger<RolePermissionsController> _logger;

    public RolePermissionsController(
        IRolePermissionAssignmentService rolePermissionService,
        ILogger<RolePermissionsController> logger)
    {
        _rolePermissionService = rolePermissionService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<Permission>>> GetRolePermissions(
        [FromRoute] int roleId,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting permissions for role ID: {RoleId}", roleId);

        try
        {
            var permissions = await _rolePermissionService.GetRolePermissionsAsync(roleId, cancellationToken);

            _logger.LogInformation("Successfully retrieved {Count} permissions for role ID: {RoleId}",
                permissions.Count, roleId);

            return Ok(permissions);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting permissions for role ID: {RoleId}", roleId);
            throw;
        }
    }

    [HttpPost("{permissionId}")]
    public async Task<IActionResult> AssignPermissionToRole(
        [FromRoute] int roleId,
        [FromRoute] int permissionId,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Assigning permission ID: {PermissionId} to role ID: {RoleId}",
            permissionId, roleId);

        try
        {
            await _rolePermissionService.AssignPermissionToRoleAsync(roleId, permissionId, cancellationToken);

            _logger.LogInformation(
                "Successfully assigned permission ID: {PermissionId} to role ID: {RoleId}",
                permissionId, roleId);

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Error assigning permission ID: {PermissionId} to role ID: {RoleId}",
                permissionId, roleId);
            throw;
        }
    }

    [HttpDelete("{permissionId}")]
    public async Task<IActionResult> RemovePermissionFromRole(
        [FromRoute] int roleId,
        [FromRoute] int permissionId,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Removing permission ID: {PermissionId} from role ID: {RoleId}",
            permissionId, roleId);

        try
        {
            await _rolePermissionService.RemovePermissionFromRoleAsync(roleId, permissionId, cancellationToken);

            _logger.LogInformation(
                "Successfully removed permission ID: {PermissionId} from role ID: {RoleId}",
                permissionId, roleId);

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Error removing permission ID: {PermissionId} from role ID: {RoleId}",
                permissionId, roleId);
            throw;
        }
    }
}