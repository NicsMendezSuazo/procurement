using Drmc.Procurement.Application.Common.Cqrs;
using MediatR;

using Drmc.Procurement.Application.Features.PlanningYearCategories.DTO;
using Drmc.Procurement.Application.Features.PlanningYearCategories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Drmc.Procurement.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlanningYearCategoriesController : ControllerBase
{
    private readonly ISender _sender;

    public PlanningYearCategoriesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _sender.Send(new GetPlanningYearCategoriesQuery()));
    }

    [HttpGet("planning-year/{planningYearId:int}")]
    public async Task<IActionResult> GetByPlanningYearId(int planningYearId)
    {
        return Ok(await _sender.Send(new GetPlanningYearCategoriesByYearQuery(planningYearId)));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _sender.Send(new GetPlanningYearCategoryQuery(id));

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Assign(
        [FromBody] AssignPlanningYearCategoryRequest request)
    {
        try
        {
            var result = await _sender.Send(new AssignPlanningYearCategoryCommand(request));

            return CreatedAtAction(
                nameof(GetById),
                new { id = result.PlanningYearCategoryId },
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

    [HttpPatch("{id:int}/active-status")]
    public async Task<IActionResult> SetActiveStatus(
        int id,
        [FromBody] bool isActive)
    {
        var updated = await _sender.Send(new SetPlanningYearCategoryActiveStatusCommand(id, isActive));

        if (!updated)
            return NotFound();

        return NoContent();
    }
}
