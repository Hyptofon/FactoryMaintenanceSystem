using Application.Interfaces.Repositories;
using Application.Interfaces.Queries;
using MediatR;

namespace Application.MaintenanceSchedules.Commands;

public class DeactivateMaintenanceScheduleCommandHandler 
    : IRequestHandler<DeactivateMaintenanceScheduleCommand, Unit>
{
    private readonly IMaintenanceScheduleRepository _repository;
    private readonly IMaintenanceScheduleQueries _queries;

    public DeactivateMaintenanceScheduleCommandHandler(
        IMaintenanceScheduleRepository repository,
        IMaintenanceScheduleQueries queries)
    {
        _repository = repository;
        _queries = queries;
    }

    public async Task<Unit> Handle(DeactivateMaintenanceScheduleCommand request, CancellationToken cancellationToken)
    {
        var schedule = await _queries.GetByIdAsync(request.Id, cancellationToken);

        if (schedule == null)
            throw new KeyNotFoundException($"Maintenance schedule with ID {request.Id} not found");

        schedule.Deactivate();
        await _repository.UpdateAsync(schedule, cancellationToken);

        return Unit.Value;
    }
}