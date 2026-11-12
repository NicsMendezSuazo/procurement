using Drmc.Procurement.Application.Common.Cqrs;
using MediatR;

using Drmc.Procurement.Application.Features.Items.DTO;
using Drmc.Procurement.Application.Features.Items.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Drmc.Procurement.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemsController : ControllerBase
{
    private readonly ISender _sender;

    public ItemsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _sender.Send(new GetItemsQuery()));
    }

    [HttpGet("category/{categoryId:int}")]
    public async Task<IActionResult> GetByCategoryId(int categoryId)
    {
        return Ok(await _sender.Send(new GetItemsByCategoryQuery(categoryId)));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _sender.Send(new GetItemQuery(id));

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpGet("planning-year/{planningYear:int}/category/{categoryId:int}")]
public async Task<IActionResult> GetByPlanningYearAndCategory(
    int planningYear,
    int categoryId)
{
    var result = await _sender.Send(new GetPlanningYearCatalogueQuery(
        planningYear,
        categoryId));

    return Ok(result);
}

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateItemRequest request)
    {
        try
        {
            var result = await _sender.Send(new CreateItemCommand(request));

            return CreatedAtAction(
                nameof(GetById),
                new { id = result.ItemId },
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
        [FromBody] UpdateItemRequest request)
    {
        try
        {
            var result = await _sender.Send(new UpdateItemCommand(id, request));

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
        var updated = await _sender.Send(new SetItemActiveStatusCommand(id, isActive));

        if (!updated)
            return NotFound();

        return NoContent();
    }
}