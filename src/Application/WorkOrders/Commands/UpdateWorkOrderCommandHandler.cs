using Application.Interfaces.Queries;
using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.WorkOrders.Commands;

public class UpdateWorkOrderCommandHandler : IRequestHandler<UpdateWorkOrderCommand, WorkOrder>
{
    private readonly IWorkOrderRepository _repository;
    private readonly IWorkOrderQueries _queries;

    public UpdateWorkOrderCommandHandler(IWorkOrderRepository repository, IWorkOrderQueries queries)
    {
        _repository = repository;
        _queries = queries;
    }

    public async Task<WorkOrder> Handle(UpdateWorkOrderCommand request, CancellationToken cancellationToken)
    {
        var workOrder = await _queries.GetByIdAsync(request.Id, cancellationToken);
        if (workOrder == null)
            throw new KeyNotFoundException($"Work order with ID {request.Id} not found");

        workOrder.UpdateDetails(request.Title, request.Description, request.Priority, request.ScheduledDate);

        return await _repository.UpdateAsync(workOrder, cancellationToken);
    }
}