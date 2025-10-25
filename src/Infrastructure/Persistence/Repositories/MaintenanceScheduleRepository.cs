using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class MaintenanceScheduleRepository : IMaintenanceScheduleRepository
{
    private readonly ApplicationDbContext _context;

    public MaintenanceScheduleRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<MaintenanceSchedule> AddAsync(MaintenanceSchedule entity, CancellationToken cancellationToken)
    {
        await _context.MaintenanceSchedules.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<MaintenanceSchedule> UpdateAsync(MaintenanceSchedule entity, CancellationToken cancellationToken)
    {
        _context.MaintenanceSchedules.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task DeleteAsync(MaintenanceSchedule entity, CancellationToken cancellationToken)
    {
        _context.MaintenanceSchedules.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
    
}