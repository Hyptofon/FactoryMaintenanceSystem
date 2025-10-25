namespace Api.DTOs.WorkOrder;

public record WorkOrderDto
{
    public Guid Id { get; init; }
    public string WorkOrderNumber { get; init; } = string.Empty;
    public Guid EquipmentId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string Priority { get; init; } = string.Empty;
    public string Status { get; init; } = string.Empty;
    public DateTime ScheduledDate { get; init; }
    public DateTime? CompletedAt { get; init; }
    public string? CompletionNotes { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }

    public static WorkOrderDto FromDomainModel(Domain.Entities.WorkOrder workOrder)
    {
        return new WorkOrderDto
        {
            Id = workOrder.Id,
            WorkOrderNumber = workOrder.WorkOrderNumber,
            EquipmentId = workOrder.EquipmentId,
            Title = workOrder.Title,
            Description = workOrder.Description,
            Priority = workOrder.Priority.ToString(),
            Status = workOrder.Status.ToString(),
            ScheduledDate = workOrder.ScheduledDate,
            CompletedAt = workOrder.CompletedAt,
            CompletionNotes = workOrder.CompletionNotes,
            CreatedAt = workOrder.CreatedAt,
            UpdatedAt = workOrder.UpdatedAt
        };
    }
}