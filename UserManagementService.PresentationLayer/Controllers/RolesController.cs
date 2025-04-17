using Microsoft.AspNetCore.Mvc;
using UserManagementService.BusinessLogicLayer.Interfaces.Users;
using UserManagementService.BusinessLogicLayer.Models.Commands;
using UserManagementService.BusinessLogicLayer.Models.Entities.Roles;

[ApiController]
[Route("api/roles")]
public class RolesController : ControllerBase
{
    private readonly IRoleService _roleService;

    public RolesController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Role>>> GetAllRoles(CancellationToken cancellationToken)
    {
        var roles = await _roleService.GetAllRolesAsync(cancellationToken);
        return Ok(roles);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Role>> GetRoleById(int id, CancellationToken cancellationToken)
    {
        var role = await _roleService.GetRoleByIdAsync(id, cancellationToken);
        return Ok(role);
    }

    [HttpPost]
    public async Task<ActionResult<Role>> CreateRole([FromBody] RoleCreateCommand command, CancellationToken cancellationToken)
    {
        await _roleService.CreateRoleAsync(command, cancellationToken);

        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Role>> UpdateRole(int id, [FromBody] RoleUpdateCommand command, CancellationToken cancellationToken)
    {
        var updatedRole = await _roleService.UpdateRoleAsync(id, command, cancellationToken);
        return Ok(updatedRole);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRole(int id, CancellationToken cancellationToken)
    {
        await _roleService.DeleteRoleAsync(id, cancellationToken);
        return NoContent();
    }
}