using Application.Interfaces.Repositories;
using Application.Interfaces.Queries;
using Domain.Entities;
using MediatR;

namespace Application.WorkOrders.Commands;

public class CreateWorkOrderCommandHandler 
    : IRequestHandler<CreateWorkOrderCommand, WorkOrder>
{
    private readonly IWorkOrderRepository _repository;
    private readonly IWorkOrderQueries _workOrderQueries;
    private readonly IEquipmentQueries _equipmentQueries;

    public CreateWorkOrderCommandHandler(
        IWorkOrderRepository repository,
        IWorkOrderQueries workOrderQueries,
        IEquipmentQueries equipmentQueries)
    {
        _repository = repository;
        _workOrderQueries = workOrderQueries;
        _equipmentQueries = equipmentQueries;
    }

    public async Task<WorkOrder> Handle(CreateWorkOrderCommand request, CancellationToken cancellationToken)
    {
        var exists = await _workOrderQueries.WorkOrderNumberExistsAsync(request.WorkOrderNumber, cancellationToken);
        if (exists)
        {
            throw new InvalidOperationException(
                $"Work order with number '{request.WorkOrderNumber}' already exists");
        }

        var equipment = await _equipmentQueries.GetByIdAsync(request.EquipmentId, cancellationToken);
        if (equipment == null)
        {
            throw new KeyNotFoundException(
                $"Equipment with ID {request.EquipmentId} not found");
        }

        var workOrder = WorkOrder.New(
            Guid.NewGuid(),
            request.WorkOrderNumber,
            request.EquipmentId,
            request.Title,
            request.Description,
            request.Priority,
            request.ScheduledDate
        );

        return await _repository.AddAsync(workOrder, cancellationToken);
    }
}