using Microsoft.AspNetCore.Mvc;
using UserManagementService.BusinessLogicLayer.Interfaces.Users;
using UserManagementService.BusinessLogicLayer.Models.Entities.Roles;

[ApiController]
[Route("api/permissions")]
public class PermissionsController : ControllerBase
{
    private readonly IPermissionService _permissionService;

    public PermissionsController(IPermissionService permissionService)
    {
        _permissionService = permissionService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Permission>>> GetAllPermissions(CancellationToken cancellationToken)
    {
        var permissions = await _permissionService.GetAllPermissionsAsync(cancellationToken);
        return Ok(permissions);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Permission>> GetPermissionById([FromHeader]int id, CancellationToken cancellationToken)
    {
        var permission = await _permissionService.GetById(id, cancellationToken);
        return Ok(permission);
    }
}
