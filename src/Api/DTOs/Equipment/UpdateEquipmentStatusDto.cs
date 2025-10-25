namespace Api.DTOs.Equipment;

public record UpdateEquipmentStatusDto
{
    public required string Status { get; init; }
}