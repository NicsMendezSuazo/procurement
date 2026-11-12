
using System.Security.Claims;

using Drmc.Procurement.Application.Features.APP.Interfaces;

using Drmc.Procurement.Application.Common.Cqrs;
using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Drmc.Procurement.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Procurement Staff,System Administrator")]
public class APPsController : ControllerBase
{
    private readonly ISender _sender;

    public APPsController(ISender sender)
    {
        _sender = sender;
    }

    private int? GetUserId()
    {
        var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);

        return int.TryParse(claim, out var userId)
            ? userId
            : null;
    }

    // GET: api/APPs
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _sender.Send(new GetAPPsQuery());
        return Ok(result);
    }

    // GET: api/APPs/1
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _sender.Send(new GetAPPQuery(id));

        if (result == null)
        {
            return NotFound(new
            {
                message = "APP not found."
            });
        }

        return Ok(result);
    }

    // POST: api/APPs/generate/1
    [HttpPost("generate/{planningYearId:int}")]
    [Authorize(Roles = "Procurement Staff")]
    public async Task<IActionResult> Generate(int planningYearId)
    {
        var userId = GetUserId();

        if (!userId.HasValue)
        {
            return Forbid();
        }

        try
        {
            var result = await _sender.Send(new GenerateAPPCommand(
                planningYearId,
                userId.Value));

            return CreatedAtAction(
                nameof(GetById),
                new { id = result.APPId },
                result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new
            {
                message = ex.Message
            });
        }
    }

    // POST: api/APPs/1/add-reviewed-ppmp
    [HttpPost("{id:int}/add-reviewed-ppmp")]
    [Authorize(Roles = "Procurement Staff")]
    public async Task<IActionResult> AddReviewedPPMPs(int id)
    {
        var userId = GetUserId();

        if (!userId.HasValue)
        {
            return Forbid();
        }

        try
        {
            var result = await _sender.Send(new AddReviewedPPMPsCommand(
                id,
                userId.Value));

            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new
            {
                message = ex.Message
            });
        }
    }
}
