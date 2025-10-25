using Domain.Entities;
using MediatR;

namespace Application.MaintenanceSchedules.Commands;

public record CreateMaintenanceScheduleCommand : IRequest<MaintenanceSchedule>
{
    public required Guid EquipmentId { get; init; }
    public required string TaskName { get; init; }
    public required string Description { get; init; }
    public required MaintenanceFrequency Frequency { get; init; }
    public required DateTime NextDueDate { get; init; }
}