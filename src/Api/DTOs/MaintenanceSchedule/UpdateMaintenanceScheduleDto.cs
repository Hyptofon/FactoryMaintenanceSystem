namespace Api.DTOs.MaintenanceSchedule;

public record UpdateMaintenanceScheduleDto
{
    public required string TaskName { get; init; }
    public required string Description { get; init; }
    public required string Frequency { get; init; }
    public required DateTime NextDueDate { get; init; }
}