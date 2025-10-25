using Application.Interfaces.Queries;
using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.WorkOrders.Commands;

public class CompleteWorkOrderCommandHandler : IRequestHandler<CompleteWorkOrderCommand, WorkOrder>
{
    private readonly IWorkOrderRepository _repository;
    private readonly IWorkOrderQueries _queries;

    public CompleteWorkOrderCommandHandler(IWorkOrderRepository repository, IWorkOrderQueries queries)
    {
        _repository = repository;
        _queries = queries;
    }

    public async Task<WorkOrder> Handle(CompleteWorkOrderCommand request, CancellationToken cancellationToken)
    {
        var workOrder = await _queries.GetByIdAsync(request.Id, cancellationToken);
        if (workOrder == null)
            throw new KeyNotFoundException($"Work order with ID {request.Id} not found");

        if (workOrder.Status != WorkOrderStatus.InProgress)
            throw new InvalidOperationException("Only in-progress work orders can be completed");

        workOrder.Complete(request.CompletionNotes);
        return await _repository.UpdateAsync(workOrder, cancellationToken);
    }
}