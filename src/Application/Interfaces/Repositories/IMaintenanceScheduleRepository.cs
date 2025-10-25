using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IMaintenanceScheduleRepository
{
    Task<MaintenanceSchedule> AddAsync(MaintenanceSchedule entity, CancellationToken cancellationToken);
    Task<MaintenanceSchedule> UpdateAsync(MaintenanceSchedule entity, CancellationToken cancellationToken);
    Task DeleteAsync(MaintenanceSchedule entity, CancellationToken cancellationToken);
   
}