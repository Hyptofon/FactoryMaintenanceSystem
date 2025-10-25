using Api.DTOs.Equipment;
using Application.Equipments.Commands;
using Application.Interfaces.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/equipment")]
public class EquipmentController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IEquipmentQueries _queries;

    public EquipmentController(IMediator mediator, IEquipmentQueries queries)
    {
        _mediator = mediator;
        _queries = queries;
    }

    /// <summary>
    /// Register new equipment
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(EquipmentDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<EquipmentDto>> CreateEquipment(
        [FromBody] CreateEquipmentDto dto,
        CancellationToken cancellationToken)
    {
        var command = new CreateEquipmentCommand
        {
            Name = dto.Name,
            Model = dto.Model,
            SerialNumber = dto.SerialNumber,
            Location = dto.Location,
            InstallationDate = dto.InstallationDate
        };

        var equipment = await _mediator.Send(command, cancellationToken);
        var result = EquipmentDto.FromDomainModel(equipment);

        return CreatedAtAction(nameof(GetEquipmentById), new { id = result.Id }, result);
    }

    /// <summary>
    /// List all equipment
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<EquipmentDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<EquipmentDto>>> GetAllEquipment(CancellationToken cancellationToken)
    {
        var equipment = await _queries.GetAllAsync(cancellationToken);
        var result = equipment.Select(EquipmentDto.FromDomainModel);
        return Ok(result);
    }

    /// <summary>
    /// Get equipment details
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(EquipmentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EquipmentDto>> GetEquipmentById(Guid id, CancellationToken cancellationToken)
    {
        var equipment = await _queries.GetByIdAsync(id, cancellationToken);
        
        if (equipment == null)
            return NotFound(new { error = $"Equipment with ID {id} not found" });

        return Ok(EquipmentDto.FromDomainModel(equipment));
    }

    /// <summary>
    /// Update equipment information
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(EquipmentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<EquipmentDto>> UpdateEquipment(
        Guid id,
        [FromBody] UpdateEquipmentDto dto,
        CancellationToken cancellationToken)
    {
        var command = new UpdateEquipmentCommand
        {
            Id = id,
            Name = dto.Name,
            Model = dto.Model,
            Location = dto.Location
        };

        var equipment = await _mediator.Send(command, cancellationToken);
        var result = EquipmentDto.FromDomainModel(equipment);

        return Ok(result);
    }

    /// <summary>
    /// Update equipment status
    /// </summary>
    [HttpPatch("{id}/status")]
    [ProducesResponseType(typeof(EquipmentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<EquipmentDto>> UpdateEquipmentStatus(
        Guid id,
        [FromBody] UpdateEquipmentStatusDto dto,
        CancellationToken cancellationToken)
    {
        if (!Enum.TryParse<Domain.Entities.EquipmentStatus>(dto.Status, true, out var status))
        {
            return BadRequest(new { error = "Invalid equipment status. Valid values: Operational, UnderMaintenance, OutOfService" });
        }

        var command = new UpdateEquipmentStatusCommand
        {
            Id = id,
            Status = status
        };

        var equipment = await _mediator.Send(command, cancellationToken);
        var result = EquipmentDto.FromDomainModel(equipment);

        return Ok(result);
    }
}