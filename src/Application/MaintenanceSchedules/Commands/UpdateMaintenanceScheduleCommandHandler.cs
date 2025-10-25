using Application.Interfaces.Queries;
using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.MaintenanceSchedules.Commands;

public class UpdateMaintenanceScheduleCommandHandler 
    : IRequestHandler<UpdateMaintenanceScheduleCommand, MaintenanceSchedule>
{
    private readonly IMaintenanceScheduleRepository _repository;
    private readonly IMaintenanceScheduleQueries _queries;

    public UpdateMaintenanceScheduleCommandHandler(
        IMaintenanceScheduleRepository repository,
        IMaintenanceScheduleQueries queries)
    {
        _repository = repository;
        _queries = queries;
    }

    public async Task<MaintenanceSchedule> Handle(UpdateMaintenanceScheduleCommand request, CancellationToken cancellationToken)
    {
        var schedule = await _queries.GetByIdAsync(request.Id, cancellationToken);

        if (schedule == null)
            throw new KeyNotFoundException($"Maintenance schedule with ID {request.Id} not found");
        
        schedule.UpdateSchedule(request.TaskName, request.Description, request.Frequency, request.NextDueDate);

        return await _repository.UpdateAsync(schedule, cancellationToken);
    }
}
