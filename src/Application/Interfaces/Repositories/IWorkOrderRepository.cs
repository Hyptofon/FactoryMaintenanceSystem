using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IWorkOrderRepository
{
    Task<WorkOrder> AddAsync(WorkOrder entity, CancellationToken cancellationToken);
    Task<WorkOrder> UpdateAsync(WorkOrder entity, CancellationToken cancellationToken);
    Task DeleteAsync(WorkOrder entity, CancellationToken cancellationToken);

}