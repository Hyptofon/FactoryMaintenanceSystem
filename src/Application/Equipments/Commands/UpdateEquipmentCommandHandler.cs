using Application.Interfaces.Repositories;
using Application.Interfaces.Queries;
using Domain.Entities;
using MediatR;

namespace Application.Equipments.Commands;

public class UpdateEquipmentCommandHandler : IRequestHandler<UpdateEquipmentCommand, Equipment>
{
    private readonly IEquipmentRepository _repository;
    private readonly IEquipmentQueries _queries;

    public UpdateEquipmentCommandHandler(IEquipmentRepository repository, IEquipmentQueries queries)
    {
        _repository = repository;
        _queries = queries;
    }

    public async Task<Equipment> Handle(UpdateEquipmentCommand request, CancellationToken cancellationToken)
    {
        var equipment = await _queries.GetByIdAsync(request.Id, cancellationToken);

        if (equipment == null)
            throw new KeyNotFoundException($"Equipment with ID {request.Id} not found");

        equipment.UpdateDetails(request.Name, request.Model, request.Location);

        return await _repository.UpdateAsync(equipment, cancellationToken);
    }
}