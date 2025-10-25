using Api.DTOs.MaintenanceSchedule;
using Application.Interfaces.Queries;
using Application.MaintenanceSchedules.Commands;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/maintenance-schedules")]
public class MaintenanceSchedulesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMaintenanceScheduleQueries _queries;

    public MaintenanceSchedulesController(IMediator mediator, IMaintenanceScheduleQueries queries)
    {
        _mediator = mediator;
        _queries = queries;
    }

    /// <summary>
    /// Create maintenance schedule
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(MaintenanceScheduleDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<MaintenanceScheduleDto>> CreateSchedule(
        [FromBody] CreateMaintenanceScheduleDto dto,
        CancellationToken cancellationToken)
    {
        if (!Enum.TryParse<MaintenanceFrequency>(dto.Frequency, true, out var frequency))
        {
            return BadRequest(new { error = "Invalid maintenance frequency. Valid values: Daily, Weekly, Monthly, Quarterly, Annually" });
        }

        var command = new CreateMaintenanceScheduleCommand
        {
            EquipmentId = dto.EquipmentId,
            TaskName = dto.TaskName,
            Description = dto.Description,
            Frequency = frequency,
            NextDueDate = dto.NextDueDate
        };

        var schedule = await _mediator.Send(command, cancellationToken);
        var result = MaintenanceScheduleDto.FromDomainModel(schedule);

        return CreatedAtAction(nameof(GetScheduleById), new { id = result.Id }, result);
    }

    /// <summary>
    /// List all schedules
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<MaintenanceScheduleDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<MaintenanceScheduleDto>>> GetAllSchedules(CancellationToken cancellationToken)
    {
        var schedules = await _queries.GetAllAsync(cancellationToken);
        var result = schedules.Select(MaintenanceScheduleDto.FromDomainModel);
        return Ok(result);
    }

    /// <summary>
    /// Get schedule by ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(MaintenanceScheduleDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MaintenanceScheduleDto>> GetScheduleById(Guid id, CancellationToken cancellationToken)
    {
        var schedule = await _queries.GetByIdAsync(id, cancellationToken);
        
        if (schedule == null)
            return NotFound(new { error = $"Schedule with ID {id} not found" });

        return Ok(MaintenanceScheduleDto.FromDomainModel(schedule));
    }

    /// <summary>
    /// Get schedules for specific equipment
    /// </summary>
    [HttpGet("equipment/{equipmentId}")]
    [ProducesResponseType(typeof(IEnumerable<MaintenanceScheduleDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<MaintenanceScheduleDto>>> GetSchedulesByEquipment(
        Guid equipmentId,
        CancellationToken cancellationToken)
    {
        var schedules = await _queries.GetByEquipmentIdAsync(equipmentId, cancellationToken);
        var result = schedules.Select(MaintenanceScheduleDto.FromDomainModel);
        return Ok(result);
    }

    /// <summary>
    /// Update schedule
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(MaintenanceScheduleDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<MaintenanceScheduleDto>> UpdateSchedule(
        Guid id,
        [FromBody] UpdateMaintenanceScheduleDto dto,
        CancellationToken cancellationToken)
    {
        if (!Enum.TryParse<MaintenanceFrequency>(dto.Frequency, true, out var frequency))
        {
            return BadRequest(new { error = "Invalid maintenance frequency. Valid values: Daily, Weekly, Monthly, Quarterly, Annually" });
        }

        var command = new UpdateMaintenanceScheduleCommand
        {
            Id = id,
            TaskName = dto.TaskName,
            Description = dto.Description,
            Frequency = frequency,
            NextDueDate = dto.NextDueDate
        };

        var schedule = await _mediator.Send(command, cancellationToken);
        var result = MaintenanceScheduleDto.FromDomainModel(schedule);

        return Ok(result);
    }

    /// <summary>
    /// Deactivate schedule
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeactivateSchedule(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeactivateMaintenanceScheduleCommand { Id = id };
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }
}