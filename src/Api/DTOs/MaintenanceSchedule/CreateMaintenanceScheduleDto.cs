namespace Api.DTOs.MaintenanceSchedule;

public record CreateMaintenanceScheduleDto
{
    public required Guid EquipmentId { get; init; }
    public required string TaskName { get; init; }
    public required string Description { get; init; }
    public required string Frequency { get; init; }
    public required DateTime NextDueDate { get; init; }
}