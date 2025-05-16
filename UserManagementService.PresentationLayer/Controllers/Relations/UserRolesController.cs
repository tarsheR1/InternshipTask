using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UserManagementService.BusinessLogicLayer.Interfaces.Users;
using UserManagementService.BusinessLogicLayer.Models.Entities.Roles;

namespace UserManagementService.PresentationLayer.Controllers.Relations
{
    [ApiController]
    [Route("api/users/{userId}/roles")]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleService _userRoleService;
        private readonly ILogger<UserRoleController> _logger;

        public UserRoleController(
            IUserRoleService userRoleService,
            ILogger<UserRoleController> logger)
        {
            _userRoleService = userRoleService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<Role>>> GetUserRoles(
            [FromRoute] Guid userId,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting roles for user ID: {UserId}", userId);

            try
            {
                var roles = await _userRoleService.GetUserRolesAsync(userId, cancellationToken);

                _logger.LogInformation("Successfully retrieved {RoleCount} roles for user ID: {UserId}",
                    roles.Count, userId);

                return Ok(roles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting roles for user ID: {UserId}", userId);
                throw;
            }
        }

        [HttpPost("{roleId}")]
        public async Task<IActionResult> AssignRole(
            [FromRoute] Guid userId,
            [FromRoute] int roleId,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Assigning role ID: {RoleId} to user ID: {UserId}",
                roleId, userId);

            try
            {
                await _userRoleService.AssignRoleToUserAsync(userId, roleId, cancellationToken);

                _logger.LogInformation(
                    "Successfully assigned role ID: {RoleId} to user ID: {UserId}",
                    roleId, userId);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Error assigning role ID: {RoleId} to user ID: {UserId}",
                    roleId, userId);
                throw;
            }
        }

        [HttpDelete("{roleId}")]
        public async Task<IActionResult> RemoveRole(
            [FromRoute] Guid userId,
            [FromRoute] int roleId,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Removing role ID: {RoleId} from user ID: {UserId}",
                roleId, userId);

            try
            {
                await _userRoleService.RemoveRoleFromUserAsync(userId, roleId, cancellationToken);

                _logger.LogInformation(
                    "Successfully removed role ID: {RoleId} from user ID: {UserId}",
                    roleId, userId);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Error removing role ID: {RoleId} from user ID: {UserId}",
                    roleId, userId);
                throw;
            }
        }
    }
}