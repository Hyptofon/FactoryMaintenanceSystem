using Application.Interfaces.Repositories;
using Application.Interfaces.Queries;
using Domain.Entities;
using MediatR;

namespace Application.Equipments.Commands;

public class CreateEquipmentCommandHandler : IRequestHandler<CreateEquipmentCommand, Equipment>
{
    private readonly IEquipmentRepository _repository;
    private readonly IEquipmentQueries _queries;

    public CreateEquipmentCommandHandler(
        IEquipmentRepository repository,
        IEquipmentQueries queries)
    {
        _repository = repository;
        _queries = queries;
    }

    public async Task<Equipment> Handle(CreateEquipmentCommand request, CancellationToken cancellationToken)
    {
        var exists = await _queries.SerialNumberExistsAsync(request.SerialNumber, cancellationToken);
        if (exists)
        {
            throw new InvalidOperationException(
                $"Equipment with serial number '{request.SerialNumber}' already exists");
        }

        var equipment = Equipment.New(
            Guid.NewGuid(),
            request.Name,
            request.Model,
            request.SerialNumber,
            request.Location,
            request.InstallationDate
        );

        return await _repository.AddAsync(equipment, cancellationToken);
    }
}