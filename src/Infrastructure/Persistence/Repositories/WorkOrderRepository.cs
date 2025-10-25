using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class WorkOrderRepository : IWorkOrderRepository
{
    private readonly ApplicationDbContext _context;

    public WorkOrderRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<WorkOrder> AddAsync(WorkOrder entity, CancellationToken cancellationToken)
    {
        await _context.WorkOrders.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<WorkOrder> UpdateAsync(WorkOrder entity, CancellationToken cancellationToken)
    {
        _context.WorkOrders.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task DeleteAsync(WorkOrder entity, CancellationToken cancellationToken)
    {
        _context.WorkOrders.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
    
}