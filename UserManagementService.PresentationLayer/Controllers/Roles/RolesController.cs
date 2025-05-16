using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;
using UserManagementService.BusinessLogicLayer.Interfaces.Users;
using UserManagementService.BusinessLogicLayer.Models.DTO.Request.Roles;

[ApiController]
[Route("api/roles")]
public class RolesController : ControllerBase
{
    private readonly IRoleService _roleService;
    private readonly IMapper _mapper;
    private readonly ILogger<RolesController> _logger;

    public RolesController(
        IRoleService roleService,
        IMapper mapper,
        ILogger<RolesController> logger)
    {
        _roleService = roleService;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllRoles(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting to retrieve all roles");

        try
        {
            var roles = await _roleService.GetAllRolesAsync(cancellationToken);

            _logger.LogInformation("Successfully retrieved {RoleCount} roles", roles.Count());

            return Ok(roles);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while retrieving all roles");
            throw;
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRoleById(
        [FromRoute] int id,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting to retrieve role with ID: {RoleId}", id);

        try
        {
            var role = await _roleService.GetRoleByIdAsync(id, cancellationToken);

            if (role == null)
            {
                _logger.LogWarning("Role with ID: {RoleId} not found", id);
                return NotFound();
            }

            _logger.LogInformation("Successfully retrieved role with ID: {RoleId}", id);
            return Ok(role);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while retrieving role with ID: {RoleId}", id);
            throw;
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateRole(
        [FromBody] RoleCreateRequestDto request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting to create new role with name: {RoleName}", request.Name);

        try
        {
            await _roleService.CreateRoleAsync(request, cancellationToken);

            _logger.LogInformation("Successfully created new role with name: {RoleName}", request.Name);

            return Ok(new { Message = "Role created successfully" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating role with name: {RoleName}", request.Name);
            throw;
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRole(
        [FromRoute] int id,
        [FromBody] RoleUpdateRequestDto request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting to update role with ID: {RoleId}", id);

        try
        {
            var updatedRole = await _roleService.UpdateRoleAsync(id, request, cancellationToken);

            if (updatedRole == null)
            {
                _logger.LogWarning("Role with ID: {RoleId} not found for update", id);
                return NotFound();
            }

            _logger.LogInformation("Successfully updated role with ID: {RoleId}", id);
            return Ok(updatedRole);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating role with ID: {RoleId}", id);
            throw;
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRole(
        [FromRoute] int id,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting to delete role with ID: {RoleId}", id);

        try
        {
            await _roleService.DeleteRoleAsync(id, cancellationToken);

            _logger.LogInformation("Successfully deleted role with ID: {RoleId}", id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting role with ID: {RoleId}", id);
            throw;
        }
    }
}