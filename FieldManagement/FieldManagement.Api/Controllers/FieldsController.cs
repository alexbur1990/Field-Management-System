using FieldManagement.Api.DTOs;
using FieldManagement.Api.Models;
using FieldManagement.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace FieldManagement.Api.Controllers;

[ApiController]
[Route("api/fields")]
public class FieldsController : ControllerBase
{
    private readonly FieldService _service;

    // For the scope of the assignment I simulated the current user. In a real system this would come from authentication middleware.
    private static readonly Guid CurrentUserId =
        Guid.Parse("11111111-1111-1111-1111-111111111111");

    public FieldsController(FieldService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<FieldDto>>> Get()
    {
        var fields = await _service.GetForUserAsync(CurrentUserId);

        return Ok(fields.Select(f => new FieldDto
        {
            Id = f.Id,
            Name = f.Name,
            Area = f.Area
        }));
    }

    [HttpPost]
    public async Task<ActionResult<FieldDto>> Create(CreateFieldDto dto)
    {
        var field = new Field
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Area = dto.Area,
            OwnerUserId = CurrentUserId
        };

        await _service.CreateAsync(field);

        return Ok(new FieldDto
        {
            Id = field.Id,
            Name = field.Name,
            Area = field.Area
        });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _service.DeleteAsync(id, CurrentUserId);
        if (!deleted) return NotFound();

        return NoContent();
    }
}