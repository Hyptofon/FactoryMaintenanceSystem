namespace Domain.Entities;

public class Equipment
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Model { get; private set; }
    public string SerialNumber { get; private set; }
    public string Location { get; private set; }
    public EquipmentStatus Status { get; private set; }
    public DateTime InstallationDate { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    
    private Equipment() { }

    private Equipment(
        Guid id,
        string name,
        string model,
        string serialNumber,
        string location,
        DateTime installationDate)
    {
        Id = id;
        Name = name;
        Model = model;
        SerialNumber = serialNumber;
        Location = location;
        Status = EquipmentStatus.Operational;
        InstallationDate = installationDate;
        CreatedAt = DateTime.UtcNow;
    }

    public static Equipment New(
        Guid id,
        string name,
        string model,
        string serialNumber,
        string location,
        DateTime installationDate)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty", nameof(name));
        
        if (string.IsNullOrWhiteSpace(model))
            throw new ArgumentException("Model cannot be empty", nameof(model));
        
        if (string.IsNullOrWhiteSpace(serialNumber))
            throw new ArgumentException("Serial number cannot be empty", nameof(serialNumber));
        
        if (string.IsNullOrWhiteSpace(location))
            throw new ArgumentException("Location cannot be empty", nameof(location));

        if (installationDate > DateTime.UtcNow)
            throw new ArgumentException("Installation date cannot be in the future", nameof(installationDate));

        return new Equipment(id, name, model, serialNumber, location, installationDate);
    }

    public void UpdateDetails(string name, string model, string location)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty", nameof(name));
        
        if (string.IsNullOrWhiteSpace(model))
            throw new ArgumentException("Model cannot be empty", nameof(model));
        
        if (string.IsNullOrWhiteSpace(location))
            throw new ArgumentException("Location cannot be empty", nameof(location));

        Name = name;
        Model = model;
        Location = location;
        UpdatedAt = DateTime.UtcNow;
    }

    public void ChangeStatus(EquipmentStatus newStatus)
    {
        Status = newStatus;
        UpdatedAt = DateTime.UtcNow;
    }
}

public enum EquipmentStatus
{
    Operational,
    UnderMaintenance,
    OutOfService
}