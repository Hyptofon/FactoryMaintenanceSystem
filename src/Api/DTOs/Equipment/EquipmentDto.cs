namespace Api.DTOs.Equipment;

public record EquipmentDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Model { get; init; } = string.Empty;
    public string SerialNumber { get; init; } = string.Empty;
    public string Location { get; init; } = string.Empty;
    public string Status { get; init; } = string.Empty;
    public DateTime InstallationDate { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }

    public static EquipmentDto FromDomainModel(Domain.Entities.Equipment equipment)
    {
        return new EquipmentDto
        {
            Id = equipment.Id,
            Name = equipment.Name,
            Model = equipment.Model,
            SerialNumber = equipment.SerialNumber,
            Location = equipment.Location,
            Status = equipment.Status.ToString(),
            InstallationDate = equipment.InstallationDate,
            CreatedAt = equipment.CreatedAt,
            UpdatedAt = equipment.UpdatedAt
        };
    }
}