using Api.DTOs.WorkOrder;
using Application.Interfaces.Queries;
using Application.WorkOrders.Commands;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/work-orders")]
public class WorkOrdersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IWorkOrderQueries _queries;

    public WorkOrdersController(IMediator mediator, IWorkOrderQueries queries)
    {
        _mediator = mediator;
        _queries = queries;
    }

    /// <summary>
    /// Create work order
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(WorkOrderDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<WorkOrderDto>> CreateWorkOrder(
        [FromBody] CreateWorkOrderDto dto,
        CancellationToken cancellationToken)
    {
        if (!Enum.TryParse<WorkOrderPriority>(dto.Priority, true, out var priority))
        {
            return BadRequest(new { error = "Invalid work order priority. Valid values: Low, Medium, High, Critical" });
        }

        var command = new CreateWorkOrderCommand
        {
            WorkOrderNumber = dto.WorkOrderNumber,
            EquipmentId = dto.EquipmentId,
            Title = dto.Title,
            Description = dto.Description,
            Priority = priority,
            ScheduledDate = dto.ScheduledDate
        };

        var workOrder = await _mediator.Send(command, cancellationToken);
        var result = WorkOrderDto.FromDomainModel(workOrder);

        return CreatedAtAction(nameof(GetWorkOrderById), new { id = result.Id }, result);
    }

    /// <summary>
    /// List all work orders
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<WorkOrderDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<WorkOrderDto>>> GetAllWorkOrders(CancellationToken cancellationToken)
    {
        var workOrders = await _queries.GetAllAsync(cancellationToken);
        var result = workOrders.Select(WorkOrderDto.FromDomainModel);
        return Ok(result);
    }

    /// <summary>
    /// Get work order details
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(WorkOrderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WorkOrderDto>> GetWorkOrderById(Guid id, CancellationToken cancellationToken)
    {
        var workOrder = await _queries.GetByIdAsync(id, cancellationToken);
        
        if (workOrder == null)
            return NotFound(new { error = $"Work order with ID {id} not found" });

        return Ok(WorkOrderDto.FromDomainModel(workOrder));
    }

    /// <summary>
    /// Update work order details
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(WorkOrderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<WorkOrderDto>> UpdateWorkOrder(
        Guid id,
        [FromBody] UpdateWorkOrderDto dto,
        CancellationToken cancellationToken)
    {
        if (!Enum.TryParse<WorkOrderPriority>(dto.Priority, true, out var priority))
        {
            return BadRequest(new { error = "Invalid work order priority. Valid values: Low, Medium, High, Critical" });
        }

        var command = new UpdateWorkOrderCommand
        {
            Id = id,
            Title = dto.Title,
            Description = dto.Description,
            Priority = priority,
            ScheduledDate = dto.ScheduledDate
        };

        var workOrder = await _mediator.Send(command, cancellationToken);
        var result = WorkOrderDto.FromDomainModel(workOrder);

        return Ok(result);
    }

    /// <summary>
    /// Start work on order
    /// </summary>
    [HttpPatch("{id}/start")]
    [ProducesResponseType(typeof(WorkOrderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<WorkOrderDto>> StartWorkOrder(Guid id, CancellationToken cancellationToken)
    {
        var command = new StartWorkOrderCommand { Id = id };
        var workOrder = await _mediator.Send(command, cancellationToken);
        var result = WorkOrderDto.FromDomainModel(workOrder);

        return Ok(result);
    }

    /// <summary>
    /// Complete work order with notes
    /// </summary>
    [HttpPost("{id}/complete")]
    [ProducesResponseType(typeof(WorkOrderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<WorkOrderDto>> CompleteWorkOrder(
        Guid id,
        [FromBody] CompleteWorkOrderDto dto,
        CancellationToken cancellationToken)
    {
        var command = new CompleteWorkOrderCommand
        {
            Id = id,
            CompletionNotes = dto.CompletionNotes
        };

        var workOrder = await _mediator.Send(command, cancellationToken);
        var result = WorkOrderDto.FromDomainModel(workOrder);

        return Ok(result);
    }

    /// <summary>
    /// Cancel work order
    /// </summary>
    [HttpPost("{id}/cancel")]
    [ProducesResponseType(typeof(WorkOrderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<WorkOrderDto>> CancelWorkOrder(Guid id, CancellationToken cancellationToken)
    {
        var command = new CancelWorkOrderCommand { Id = id };
        var workOrder = await _mediator.Send(command, cancellationToken);
        var result = WorkOrderDto.FromDomainModel(workOrder);

        return Ok(result);
    }
}