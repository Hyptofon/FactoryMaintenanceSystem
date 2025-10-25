namespace Api.DTOs.WorkOrder;

public record CompleteWorkOrderDto
{
    public required string CompletionNotes { get; init; }
}