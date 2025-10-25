using Domain.Entities;
using MediatR;

namespace Application.WorkOrders.Commands;

public record UpdateWorkOrderCommand : IRequest<WorkOrder>
{
    public required Guid Id { get; init; }
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required WorkOrderPriority Priority { get; init; }
    public required DateTime ScheduledDate { get; init; }
}