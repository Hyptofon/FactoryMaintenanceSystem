using Application.Interfaces.Repositories;
using Application.Interfaces.Queries;
using Domain.Entities;
using MediatR;

namespace Application.MaintenanceSchedules.Commands;

public class CreateMaintenanceScheduleCommandHandler 
    : IRequestHandler<CreateMaintenanceScheduleCommand, MaintenanceSchedule>
{
    private readonly IMaintenanceScheduleRepository _repository;
    private readonly IEquipmentQueries _equipmentQueries;

    public CreateMaintenanceScheduleCommandHandler(
        IMaintenanceScheduleRepository repository,
        IEquipmentQueries equipmentQueries)
    {
        _repository = repository;
        _equipmentQueries = equipmentQueries;
    }

    public async Task<MaintenanceSchedule> Handle(CreateMaintenanceScheduleCommand request, CancellationToken cancellationToken)
    {
        var equipment = await _equipmentQueries.GetByIdAsync(request.EquipmentId, cancellationToken);
        if (equipment == null)
        {
            throw new KeyNotFoundException($"Equipment with ID {request.EquipmentId} not found");
        }

        var schedule = MaintenanceSchedule.New(
            Guid.NewGuid(),
            request.EquipmentId,
            request.TaskName,
            request.Description,
            request.Frequency,
            request.NextDueDate
        );

        return await _repository.AddAsync(schedule, cancellationToken);
    }
}