using Drmc.Procurement.Application.Common.Cqrs;
using MediatR;

using Drmc.Procurement.Application.Features.Users.DTO;
using Drmc.Procurement.Application.Features.Users.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Drmc.Procurement.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly ISender _sender;

    public UsersController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _sender.Send(new GetUsersQuery()));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _sender.Send(new GetUserQuery(id));

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateUserRequest request)
    {
        try
        {
            var result = await _sender.Send(new CreateUserCommand(request));

            return CreatedAtAction(
                nameof(GetById),
                new { id = result.UserId },
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

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] CreateUserRequest request)
    {
        try
        {
            var result = await _sender.Send(new UpdateUserCommand(id, request));

            if (result is null)
                return NotFound();

            return Ok(result);
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

    [HttpPatch("{id:int}/active-status")]
    public async Task<IActionResult> SetActiveStatus(
        int id,
        [FromBody] bool isActive)
    {
        var updated = await _sender.Send(new SetUserActiveStatusCommand(id, isActive));

        if (!updated)
            return NotFound();

        return NoContent();
    }
}