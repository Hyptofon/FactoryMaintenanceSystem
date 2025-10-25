using Application.Interfaces.Repositories;
using Application.Interfaces.Queries;
using Domain.Entities;
using MediatR;

namespace Application.Equipments.Commands;

public class UpdateEquipmentStatusCommandHandler : IRequestHandler<UpdateEquipmentStatusCommand, Equipment>
{
    private readonly IEquipmentRepository _repository;
    private readonly IEquipmentQueries _queries;

    public UpdateEquipmentStatusCommandHandler(IEquipmentRepository repository, IEquipmentQueries queries)
    {
        _repository = repository;
        _queries = queries;
    }

    public async Task<Equipment> Handle(UpdateEquipmentStatusCommand request, CancellationToken cancellationToken)
    {
        var equipment = await _queries.GetByIdAsync(request.Id, cancellationToken);

        if (equipment == null)
            throw new KeyNotFoundException($"Equipment with ID {request.Id} not found");

        equipment.ChangeStatus(request.Status);

        return await _repository.UpdateAsync(equipment, cancellationToken);
    }
}