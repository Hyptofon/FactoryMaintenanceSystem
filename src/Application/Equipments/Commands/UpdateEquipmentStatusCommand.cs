using Domain.Entities;
using MediatR;

namespace Application.Equipments.Commands;

public record UpdateEquipmentStatusCommand : IRequest<Equipment>
{
    public required Guid Id { get; init; }
    public required EquipmentStatus Status { get; init; }
}