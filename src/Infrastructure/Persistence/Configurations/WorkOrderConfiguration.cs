using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class WorkOrderConfiguration : IEntityTypeConfiguration<WorkOrder>
{
    public void Configure(EntityTypeBuilder<WorkOrder> builder)
    {
        builder.ToTable("work_orders");
        
        builder.HasKey(wo => wo.Id);
        
        builder.Property(wo => wo.Id)
            .HasColumnName("id");
        
        builder.Property(wo => wo.WorkOrderNumber)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnName("work_order_number");
        
        builder.Property(wo => wo.EquipmentId)
            .IsRequired()
            .HasColumnName("equipment_id");
        
        builder.Property(wo => wo.Title)
            .IsRequired()
            .HasMaxLength(200)
            .HasColumnName("title");
        
        builder.Property(wo => wo.Description)
            .IsRequired()
            .HasMaxLength(1000)
            .HasColumnName("description");
        
        builder.Property(wo => wo.Priority)
            .IsRequired()
            .HasConversion<string>()
            .HasColumnName("priority");
        
        builder.Property(wo => wo.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasColumnName("status");
        
        builder.Property(wo => wo.ScheduledDate)
            .IsRequired()
            .HasColumnName("scheduled_date");
        
        builder.Property(wo => wo.CompletedAt)
            .HasColumnName("completed_at");
        
        builder.Property(wo => wo.CompletionNotes)
            .HasMaxLength(1000)
            .HasColumnName("completion_notes");
        
        builder.Property(wo => wo.CreatedAt)
            .IsRequired()
            .HasColumnName("created_at");
        
        builder.Property(wo => wo.UpdatedAt)
            .HasColumnName("updated_at");
        
        builder.HasIndex(wo => wo.WorkOrderNumber)
            .IsUnique()
            .HasDatabaseName("ix_work_orders_work_order_number");
        
        builder.HasIndex(wo => wo.EquipmentId)
            .HasDatabaseName("ix_work_orders_equipment_id");
        
        builder.HasIndex(wo => wo.Status)
            .HasDatabaseName("ix_work_orders_status");
    }
}