namespace Api.DTOs.Equipment;

public record CreateEquipmentDto
{
    public required string Name { get; init; }
    public required string Model { get; init; }
    public required string SerialNumber { get; init; }
    public required string Location { get; init; }
    public required DateTime InstallationDate { get; init; }
}