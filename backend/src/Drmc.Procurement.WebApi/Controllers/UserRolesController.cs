using Drmc.Procurement.Application.Common.Cqrs;
using MediatR;

using Drmc.Procurement.Application.Features.UserRoles.DTO;
using Drmc.Procurement.Application.Features.UserRoles.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Drmc.Procurement.WebApi.Controllers;

[ApiController]
[Route("api/Users/{userId:int}/roles")]
public class UserRolesController : ControllerBase
{
    private readonly ISender _sender;

    public UserRolesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> GetUserRoles(int userId)
    {
        try
        {
            return Ok(await _sender.Send(new GetUserRolesQuery(userId)));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> AssignRole(
        int userId,
        [FromBody] AssignUserRoleRequest request)
    {
        try
        {
            var result = await _sender.Send(new AssignUserRoleCommand(userId, request));

            return CreatedAtAction(
                nameof(GetUserRoles),
                new { userId },
                result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpDelete("{roleId:int}")]
    public async Task<IActionResult> RemoveRole(int userId, int roleId)
    {
        var removed = await _sender.Send(new RemoveUserRoleCommand(userId, roleId));

        if (!removed)
            return NotFound(new
            {
                message = "User role assignment not found."
            });

        return NoContent();
    }
}