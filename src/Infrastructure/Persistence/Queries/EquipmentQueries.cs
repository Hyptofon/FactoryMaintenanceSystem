using Application.Interfaces.Queries;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Queries;

public class EquipmentQueries : IEquipmentQueries
{
    private readonly ApplicationDbContext _context;

    public EquipmentQueries(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Equipment>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Equipment
            .AsNoTracking()
            .OrderBy(e => e.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<Equipment?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Equipment
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }
    
    public async Task<bool> SerialNumberExistsAsync(string serialNumber, CancellationToken cancellationToken)
    {
        return await _context.Equipment
            .AnyAsync(e => e.SerialNumber == serialNumber, cancellationToken);
    }
}