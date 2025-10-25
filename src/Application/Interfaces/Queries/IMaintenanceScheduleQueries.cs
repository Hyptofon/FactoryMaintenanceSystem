using Domain.Entities;

namespace Application.Interfaces.Queries;

public interface IMaintenanceScheduleQueries
{
    Task<IReadOnlyList<MaintenanceSchedule>> GetAllAsync(CancellationToken cancellationToken);
    Task<MaintenanceSchedule?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<MaintenanceSchedule>> GetByEquipmentIdAsync(Guid equipmentId, CancellationToken cancellationToken);
}