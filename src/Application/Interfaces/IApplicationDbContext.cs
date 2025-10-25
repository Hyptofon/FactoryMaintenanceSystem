using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Equipment> Equipment { get; }
    DbSet<MaintenanceSchedule> MaintenanceSchedules { get; }
    DbSet<WorkOrder> WorkOrders { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}