namespace Api.DTOs.Equipment;

public record UpdateEquipmentDto
{
    public required string Name { get; init; }
    public required string Model { get; init; }
    public required string Location { get; init; }
}