namespace Api.DTOs.WorkOrder;

public record UpdateWorkOrderDto
{
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required string Priority { get; init; }
    public required DateTime ScheduledDate { get; init; }
}