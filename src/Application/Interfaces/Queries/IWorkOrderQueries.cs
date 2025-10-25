using Domain.Entities;

namespace Application.Interfaces.Queries;

public interface IWorkOrderQueries
{
    
    Task<IReadOnlyList<WorkOrder>> GetAllAsync(CancellationToken cancellationToken);
    Task<WorkOrder?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<WorkOrder>> GetByStatusAsync(WorkOrderStatus status, CancellationToken cancellationToken);
    
    Task<bool> WorkOrderNumberExistsAsync(string workOrderNumber, CancellationToken cancellationToken);
}
