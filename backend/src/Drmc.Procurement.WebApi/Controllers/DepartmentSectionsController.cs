using Drmc.Procurement.Application.Common.Cqrs;
using MediatR;

using Drmc.Procurement.Application.Features.DepartmentSections.DTO;
using Drmc.Procurement.Application.Features.DepartmentSections.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Drmc.Procurement.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartmentSectionsController : ControllerBase
{
    private readonly ISender _sender;

    public DepartmentSectionsController(ISender sender)
    {
        _sender = sender;
    }

    // GET: api/departmentsections
    [HttpGet]
    public async Task<ActionResult<List<DepartmentSectionDto>>> GetAll()
    {
        var sections = await _sender.Send(new GetDepartmentSectionsQuery());

        return Ok(sections);
    }

    // GET: api/departmentsections/department/1
    [HttpGet("department/{departmentId:int}")]
    public async Task<ActionResult<List<DepartmentSectionDto>>>
        GetByDepartmentId(int departmentId)
    {
        var sections = await _sender.Send(new GetDepartmentSectionsByDepartmentQuery(
            departmentId));

        return Ok(sections);
    }

    // GET: api/departmentsections/1
    [HttpGet("{id:int}")]
    public async Task<ActionResult<DepartmentSectionDto>> GetById(int id)
    {
        var section = await _sender.Send(new GetDepartmentSectionQuery(id));

        if (section is null)
        {
            return NotFound(new
            {
                message = "Department section not found."
            });
        }

        return Ok(section);
    }

    // POST: api/departmentsections
    [HttpPost]
    public async Task<ActionResult<DepartmentSectionDto>> Create(
        [FromBody] CreateDepartmentSectionRequest request)
    {
        try
        {
            var section = await _sender.Send(new CreateDepartmentSectionCommand(request));

            return CreatedAtAction(
                nameof(GetById),
                new { id = section.SectionId },
                section);
        }
        catch (KeyNotFoundException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    // PUT: api/departmentsections/1
    [HttpPut("{id:int}")]
    public async Task<ActionResult<DepartmentSectionDto>> Update(
        int id,
        [FromBody] CreateDepartmentSectionRequest request)
    {
        try
        {
            var section = await _sender.Send(new UpdateDepartmentSectionCommand(id, request));

            if (section is null)
            {
                return NotFound(new
                {
                    message = "Department section not found."
                });
            }

            return Ok(section);
        }
        catch (KeyNotFoundException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    // PATCH: api/departmentsections/1/active-status
    [HttpPatch("{id:int}/active-status")]
    public async Task<IActionResult> SetActiveStatus(
        int id,
        [FromBody] bool isActive)
    {
        var updated = await _sender.Send(new SetDepartmentSectionActiveStatusCommand(
            id,
            isActive));

        if (!updated)
        {
            return NotFound(new
            {
                message = "Department section not found."
            });
        }

        return NoContent();
    }
}