namespace Domain.Entities;

public class WorkOrder
{
    public Guid Id { get; private set; }
    public string WorkOrderNumber { get; private set; }
    public Guid EquipmentId { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public WorkOrderPriority Priority { get; private set; }
    public WorkOrderStatus Status { get; private set; }
    public DateTime ScheduledDate { get; private set; }
    public DateTime? CompletedAt { get; private set; }
    public string? CompletionNotes { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private WorkOrder() { }

    private WorkOrder(
        Guid id,
        string workOrderNumber,
        Guid equipmentId,
        string title,
        string description,
        WorkOrderPriority priority,
        DateTime scheduledDate)
    {
        Id = id;
        WorkOrderNumber = workOrderNumber;
        EquipmentId = equipmentId;
        Title = title;
        Description = description;
        Priority = priority;
        Status = WorkOrderStatus.Open;
        ScheduledDate = scheduledDate;
        CreatedAt = DateTime.UtcNow;
    }

    public static WorkOrder New(
        Guid id,
        string workOrderNumber,
        Guid equipmentId,
        string title,
        string description,
        WorkOrderPriority priority,
        DateTime scheduledDate)
    {
        if (string.IsNullOrWhiteSpace(workOrderNumber))
            throw new ArgumentException("Work order number cannot be empty", nameof(workOrderNumber));
        
        if (equipmentId == Guid.Empty)
            throw new ArgumentException("Equipment ID cannot be empty", nameof(equipmentId));
        
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty", nameof(title));
        
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description cannot be empty", nameof(description));

        return new WorkOrder(id, workOrderNumber, equipmentId, title, description, priority, scheduledDate);
    }

    public void UpdateDetails(
        string title,
        string description,
        WorkOrderPriority priority,
        DateTime scheduledDate)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty", nameof(title));
        
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description cannot be empty", nameof(description));

        Title = title;
        Description = description;
        Priority = priority;
        ScheduledDate = scheduledDate;
        UpdatedAt = DateTime.UtcNow;
    }

    public void StartWork()
    {
        if (Status != WorkOrderStatus.Open)
            throw new InvalidOperationException("Only open work orders can be started");

        Status = WorkOrderStatus.InProgress;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Complete(string completionNotes)
    {
        if (Status == WorkOrderStatus.Completed)
            throw new InvalidOperationException("Work order is already completed");
        
        if (Status == WorkOrderStatus.Cancelled)
            throw new InvalidOperationException("Cannot complete a cancelled work order");

        Status = WorkOrderStatus.Completed;
        CompletionNotes = completionNotes;
        CompletedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Cancel()
    {
        if (Status == WorkOrderStatus.Completed)
            throw new InvalidOperationException("Cannot cancel a completed work order");
        
        if (Status == WorkOrderStatus.Cancelled)
            throw new InvalidOperationException("Work order is already cancelled");

        Status = WorkOrderStatus.Cancelled;
        UpdatedAt = DateTime.UtcNow;
    }
}

public enum WorkOrderPriority
{
    Low,
    Medium,
    High,
    Critical
}

public enum WorkOrderStatus
{
    Open,
    InProgress,
    Completed,
    Cancelled
}