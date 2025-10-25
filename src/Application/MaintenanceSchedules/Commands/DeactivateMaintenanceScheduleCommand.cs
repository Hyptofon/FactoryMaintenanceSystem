using MediatR;

namespace Application.MaintenanceSchedules.Commands;

public record DeactivateMaintenanceScheduleCommand : IRequest<Unit>
{
    public required Guid Id { get; init; }
}