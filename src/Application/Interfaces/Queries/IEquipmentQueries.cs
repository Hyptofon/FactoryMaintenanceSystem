using Domain.Entities;

namespace Application.Interfaces.Queries;

public interface IEquipmentQueries
{
    Task<IReadOnlyList<Equipment>> GetAllAsync(CancellationToken cancellationToken);
    Task<Equipment> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> SerialNumberExistsAsync(string serialNumber, CancellationToken cancellationToken);
}