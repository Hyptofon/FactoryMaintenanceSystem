namespace Api.DTOs.MaintenanceSchedule;

public record MaintenanceScheduleDto
{
    public Guid Id { get; init; }
    public Guid EquipmentId { get; init; }
    public string TaskName { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string Frequency { get; init; } = string.Empty;
    public DateTime NextDueDate { get; init; }
    public bool IsActive { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }

    public static MaintenanceScheduleDto FromDomainModel(Domain.Entities.MaintenanceSchedule schedule)
    {
        return new MaintenanceScheduleDto
        {
            Id = schedule.Id,
            EquipmentId = schedule.EquipmentId,
            TaskName = schedule.TaskName,
            Description = schedule.Description,
            Frequency = schedule.Frequency.ToString(),
            NextDueDate = schedule.NextDueDate,
            IsActive = schedule.IsActive,
            CreatedAt = schedule.CreatedAt,
            UpdatedAt = schedule.UpdatedAt
        };
    }
}