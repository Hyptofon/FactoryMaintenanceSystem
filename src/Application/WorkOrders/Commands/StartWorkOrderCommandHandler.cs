using Application.Interfaces.Queries;
using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.WorkOrders.Commands;

public class StartWorkOrderCommandHandler : IRequestHandler<StartWorkOrderCommand, WorkOrder>
{
    private readonly IWorkOrderRepository _repository;
    private readonly IWorkOrderQueries _queries;

    public StartWorkOrderCommandHandler(IWorkOrderRepository repository, IWorkOrderQueries queries)
    {
        _repository = repository;
        _queries = queries;
    }

    public async Task<WorkOrder> Handle(StartWorkOrderCommand request, CancellationToken cancellationToken)
    {
        var workOrder = await _queries.GetByIdAsync(request.Id, cancellationToken);
        if (workOrder == null)
            throw new KeyNotFoundException($"Work order with ID {request.Id} not found");

        var activeOrders = await _queries.GetByStatusAsync(WorkOrderStatus.InProgress, cancellationToken);
        if (activeOrders.Any(o => o.EquipmentId == workOrder.EquipmentId))
            throw new InvalidOperationException("Another work order is already in progress for this equipment");

        workOrder.StartWork();
        return await _repository.UpdateAsync(workOrder, cancellationToken);
    }
}