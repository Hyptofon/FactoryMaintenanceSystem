using Domain.Entities;
using MediatR;

namespace Application.WorkOrders.Commands;

public record CreateWorkOrderCommand : IRequest<WorkOrder>
{
    public required string WorkOrderNumber { get; init; }
    public required Guid EquipmentId { get; init; }
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required WorkOrderPriority Priority { get; init; }
    public required DateTime ScheduledDate { get; init; }
}