using Application.Interfaces.Queries;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Queries;

public class MaintenanceScheduleQueries : IMaintenanceScheduleQueries
{
    private readonly ApplicationDbContext _context;

    public MaintenanceScheduleQueries(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<MaintenanceSchedule>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.MaintenanceSchedules
            .AsNoTracking()
            .Where(ms => ms.IsActive)
            .OrderBy(ms => ms.NextDueDate)
            .ToListAsync(cancellationToken);
    }

    public async Task<MaintenanceSchedule?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.MaintenanceSchedules
            .AsNoTracking()
            .FirstOrDefaultAsync(ms => ms.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<MaintenanceSchedule>> GetByEquipmentIdAsync(
        Guid equipmentId, 
        CancellationToken cancellationToken)
    {
        return await _context.MaintenanceSchedules
            .AsNoTracking()
            .Where(ms => ms.EquipmentId == equipmentId && ms.IsActive)
            .OrderBy(ms => ms.NextDueDate)
            .ToListAsync(cancellationToken);
    }
    
    
    
}