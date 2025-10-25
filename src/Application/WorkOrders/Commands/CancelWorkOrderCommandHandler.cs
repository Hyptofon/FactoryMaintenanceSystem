using Application.Common.Exceptions;
using Application.Interfaces.Queries;
using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.WorkOrders.Commands;

public class CancelWorkOrderCommandHandler : IRequestHandler<CancelWorkOrderCommand, WorkOrder>
{
    private readonly IWorkOrderRepository _repository;
    private readonly IWorkOrderQueries _queries;

    public CancelWorkOrderCommandHandler(IWorkOrderRepository repository, IWorkOrderQueries queries)
    {
        _repository = repository;
        _queries = queries;
    }

    public async Task<WorkOrder> Handle(CancelWorkOrderCommand request, CancellationToken cancellationToken)
    {
        var workOrder = await _queries.GetByIdAsync(request.Id, cancellationToken);
        if (workOrder == null)
            throw new KeyNotFoundException($"Work order with ID {request.Id} not found");

        if (workOrder.Status == WorkOrderStatus.Completed)
            throw new ConflictException("Cannot cancel a completed work order");

        workOrder.Cancel();
        return await _repository.UpdateAsync(workOrder, cancellationToken);
    }
}