using Drmc.Procurement.Application.Common.Cqrs;
using MediatR;

using Drmc.Procurement.Application.Features.Departments.DTO;
using Drmc.Procurement.Application.Features.Departments.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Drmc.Procurement.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartmentsController : ControllerBase
{
    private readonly ISender _sender;

    public DepartmentsController(ISender sender)
    {
        _sender = sender;
    }

    // GET: api/departments
    [HttpGet]
    public async Task<ActionResult<List<DepartmentDto>>> GetAll()
    {
        var departments = await _sender.Send(new GetDepartmentsQuery());

        return Ok(departments);
    }

    // GET: api/departments/1
    [HttpGet("{id:int}")]
    public async Task<ActionResult<DepartmentDto>> GetById(int id)
    {
        var department = await _sender.Send(new GetDepartmentQuery(id));

        if (department is null)
        {
            return NotFound(new
            {
                message = "Department not found."
            });
        }

        return Ok(department);
    }

    // POST: api/departments
    [HttpPost]
    public async Task<ActionResult<DepartmentDto>> Create(
        [FromBody] CreateDepartmentRequest request)
    {
        try
        {
            var department = await _sender.Send(new CreateDepartmentCommand(request));

            return CreatedAtAction(
                nameof(GetById),
                new { id = department.DepartmentId },
                department);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    // PUT: api/departments/1
    [HttpPut("{id:int}")]
    public async Task<ActionResult<DepartmentDto>> Update(
        int id,
        [FromBody] CreateDepartmentRequest request)
    {
        try
        {
            var department = await _sender.Send(new UpdateDepartmentCommand(id, request));

            if (department is null)
            {
                return NotFound(new
                {
                    message = "Department not found."
                });
            }

            return Ok(department);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    // PATCH: api/departments/1/active-status
    [HttpPatch("{id:int}/active-status")]
    public async Task<IActionResult> SetActiveStatus(
        int id,
        [FromBody] bool isActive)
    {
        var updated = await _sender.Send(new SetDepartmentActiveStatusCommand(
            id,
            isActive));

        if (!updated)
        {
            return NotFound(new
            {
                message = "Department not found."
            });
        }

        return NoContent();
    }
} 