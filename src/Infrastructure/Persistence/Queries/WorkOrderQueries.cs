using Application.Interfaces.Queries;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Queries;

public class WorkOrderQueries : IWorkOrderQueries
{
    private readonly ApplicationDbContext _context;

    public WorkOrderQueries(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<WorkOrder>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.WorkOrders
            .AsNoTracking()
            .OrderByDescending(wo => wo.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<WorkOrder?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.WorkOrders
            .AsNoTracking()
            .FirstOrDefaultAsync(wo => wo.Id == id, cancellationToken);
    }
    
    public async Task<IReadOnlyList<WorkOrder>> GetByStatusAsync(WorkOrderStatus status, CancellationToken cancellationToken)
    {
        return await _context.WorkOrders
            .AsNoTracking()
            .Where(wo => wo.Status == status)
            .OrderBy(wo => wo.ScheduledDate)
            .ToListAsync(cancellationToken);
    }
    
    public async Task<bool> WorkOrderNumberExistsAsync(string workOrderNumber, CancellationToken cancellationToken)
    {
        return await _context.WorkOrders
            .AnyAsync(wo => wo.WorkOrderNumber == workOrderNumber, cancellationToken);
    }
}