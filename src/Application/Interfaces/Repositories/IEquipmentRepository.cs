using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IEquipmentRepository
{
    Task<Equipment> AddAsync(Equipment entity, CancellationToken cancellationToken);
    Task<Equipment> UpdateAsync(Equipment entity, CancellationToken cancellationToken);
    Task DeleteAsync(Equipment entity, CancellationToken cancellationToken);
    
}