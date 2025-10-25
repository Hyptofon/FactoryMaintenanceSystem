using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class EquipmentRepository : IEquipmentRepository
{
    private readonly ApplicationDbContext _context;

    public EquipmentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Equipment> AddAsync(Equipment entity, CancellationToken cancellationToken)
    {
        await _context.Equipment.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<Equipment> UpdateAsync(Equipment entity, CancellationToken cancellationToken)
    {
        _context.Equipment.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task DeleteAsync(Equipment entity, CancellationToken cancellationToken)
    {
        _context.Equipment.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}