using Domain.Entities;
using MediatR;

namespace Application.Equipments.Commands;

public record UpdateEquipmentCommand : IRequest<Equipment>
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Model { get; init; }
    public required string Location { get; init; }
}