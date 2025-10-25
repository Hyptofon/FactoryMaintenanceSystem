namespace Api.DTOs.WorkOrder;

public record CreateWorkOrderDto
{
    public required string WorkOrderNumber { get; init; }
    public required Guid EquipmentId { get; init; }
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required string Priority { get; init; }
    public required DateTime ScheduledDate { get; init; }
}