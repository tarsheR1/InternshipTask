using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UserManagementService.BusinessLogicLayer.Interfaces.Users;
using UserManagementService.BusinessLogicLayer.Models.Commands;
using UserManagementService.PresentationLayer.DTO.Request;

[ApiController]
[Route("api/roles")]
public class RolesController : ControllerBase
{
    private readonly IRoleService _roleService;
    private readonly IMapper _mapper;

    public RolesController(IRoleService roleService, IMapper mapper)
    {
        _roleService = roleService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllRoles(CancellationToken cancellationToken)
    {
        var roles = await _roleService.GetAllRolesAsync(cancellationToken);
        return Ok(roles);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRoleById(
        [FromRoute] int id,
        CancellationToken cancellationToken)
    {
        var role = await _roleService.GetRoleByIdAsync(id, cancellationToken);
        return Ok(role);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRole(
        [FromBody] RoleCreateCommand command,
        CancellationToken cancellationToken)
    {
        await _roleService.CreateRoleAsync(command, cancellationToken);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRole(
        [FromRoute] int id,
        [FromBody] RoleUpdateRequest request,
        CancellationToken cancellationToken)
    {
        var command = _mapper.Map<RoleUpdateCommand>(request);
        var updatedRole = await _roleService.UpdateRoleAsync(id, command, cancellationToken);
        return Ok(updatedRole);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRole(
        [FromRoute] int id,
        CancellationToken cancellationToken)
    {
        await _roleService.DeleteRoleAsync(id, cancellationToken);
        return NoContent();
    }
}