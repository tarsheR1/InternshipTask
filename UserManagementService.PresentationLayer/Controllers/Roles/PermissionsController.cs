using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UserManagementService.BusinessLogicLayer.Interfaces.Users;
using UserManagementService.BusinessLogicLayer.Models.Entities.Roles;

[ApiController]
[Route("api/permissions")]
public class PermissionsController : ControllerBase
{
    private readonly IPermissionService _permissionService;
    private readonly ILogger<PermissionsController> _logger;

    public PermissionsController(
        IPermissionService permissionService,
        ILogger<PermissionsController> logger)
    {
        _permissionService = permissionService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<Permission>>> GetAllPermissions(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting to retrieve all permissions");

        try
        {
            var permissions = await _permissionService.GetAllPermissionsAsync(cancellationToken);

            _logger.LogInformation("Successfully retrieved {PermissionCount} permissions", permissions.Count);

            return Ok(permissions);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while retrieving all permissions");
            throw;
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Permission>> GetPermissionById(
        [FromRoute] int id,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting to retrieve permission with ID: {PermissionId}", id);

        try
        {
            var permission = await _permissionService.GetPermissionByIdAsync(id, cancellationToken);

            if (permission == null)
            {
                _logger.LogWarning("Permission with ID: {PermissionId} not found", id);
                return NotFound();
            }

            _logger.LogInformation("Successfully retrieved permission with ID: {PermissionId}", id);
            return Ok(permission);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while retrieving permission with ID: {PermissionId}", id);
            throw;
        }
    }
}