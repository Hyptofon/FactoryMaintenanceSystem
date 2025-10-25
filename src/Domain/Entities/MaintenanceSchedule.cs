namespace Domain.Entities;

public class MaintenanceSchedule
{
    public Guid Id { get; private set; }
    public Guid EquipmentId { get; private set; }
    public string TaskName { get; private set; }
    public string Description { get; private set; }
    public MaintenanceFrequency Frequency { get; private set; }
    public DateTime NextDueDate { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }


    private MaintenanceSchedule() { }

    private MaintenanceSchedule(
        Guid id,
        Guid equipmentId,
        string taskName,
        string description,
        MaintenanceFrequency frequency,
        DateTime nextDueDate)
    {
        Id = id;
        EquipmentId = equipmentId;
        TaskName = taskName;
        Description = description;
        Frequency = frequency;
        NextDueDate = nextDueDate;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
    }

    public static MaintenanceSchedule New(
        Guid id,
        Guid equipmentId,
        string taskName,
        string description,
        MaintenanceFrequency frequency,
        DateTime nextDueDate)
    {
        if (equipmentId == Guid.Empty)
            throw new ArgumentException("Equipment ID cannot be empty", nameof(equipmentId));
        
        if (string.IsNullOrWhiteSpace(taskName))
            throw new ArgumentException("Task name cannot be empty", nameof(taskName));
        
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description cannot be empty", nameof(description));
        
        if (nextDueDate <= DateTime.UtcNow)
            throw new ArgumentException("Next due date must be in the future", nameof(nextDueDate));

        return new MaintenanceSchedule(id, equipmentId, taskName, description, frequency, nextDueDate);
    }

    public void UpdateSchedule(
        string taskName,
        string description,
        MaintenanceFrequency frequency,
        DateTime nextDueDate)
    {
        if (string.IsNullOrWhiteSpace(taskName))
            throw new ArgumentException("Task name cannot be empty", nameof(taskName));
        
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description cannot be empty", nameof(description));
        
        if (nextDueDate <= DateTime.UtcNow)
            throw new ArgumentException("Next due date must be in the future", nameof(nextDueDate));

        TaskName = taskName;
        Description = description;
        Frequency = frequency;
        NextDueDate = nextDueDate;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }
}

public enum MaintenanceFrequency
{
    Daily,
    Weekly,
    Monthly,
    Quarterly,
    Annually
}