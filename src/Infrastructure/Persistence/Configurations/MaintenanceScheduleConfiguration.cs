using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class MaintenanceScheduleConfiguration : IEntityTypeConfiguration<MaintenanceSchedule>
{
    public void Configure(EntityTypeBuilder<MaintenanceSchedule> builder)
    {
        builder.ToTable("maintenance_schedules");
        
        builder.HasKey(ms => ms.Id);
        
        builder.Property(ms => ms.Id)
            .HasColumnName("id");
        
        builder.Property(ms => ms.EquipmentId)
            .IsRequired()
            .HasColumnName("equipment_id");
        
        builder.Property(ms => ms.TaskName)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("task_name");
        
        builder.Property(ms => ms.Description)
            .IsRequired()
            .HasMaxLength(500)
            .HasColumnName("description");
        
        builder.Property(ms => ms.Frequency)
            .IsRequired()
            .HasConversion<string>()
            .HasColumnName("frequency");
        
        builder.Property(ms => ms.NextDueDate)
            .IsRequired()
            .HasColumnName("next_due_date");
        
        builder.Property(ms => ms.IsActive)
            .IsRequired()
            .HasColumnName("is_active");
        
        builder.Property(ms => ms.CreatedAt)
            .IsRequired()
            .HasColumnName("created_at");
        
        builder.Property(ms => ms.UpdatedAt)
            .HasColumnName("updated_at");
        
        builder.HasIndex(ms => ms.EquipmentId)
            .HasDatabaseName("ix_maintenance_schedules_equipment_id");
        
        builder.HasIndex(ms => ms.NextDueDate)
            .HasDatabaseName("ix_maintenance_schedules_next_due_date");
    }
}