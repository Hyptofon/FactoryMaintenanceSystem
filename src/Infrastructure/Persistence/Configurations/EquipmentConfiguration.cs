using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class EquipmentConfiguration : IEntityTypeConfiguration<Equipment>
{
    public void Configure(EntityTypeBuilder<Equipment> builder)
    {
        builder.ToTable("equipment");
        
        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.Id)
            .HasColumnName("id");
        
        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("name");
        
        builder.Property(e => e.Model)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnName("model");
        
        builder.Property(e => e.SerialNumber)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnName("serial_number");
        
        builder.Property(e => e.Location)
            .IsRequired()
            .HasMaxLength(200)
            .HasColumnName("location");
        
        builder.Property(e => e.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasColumnName("status");
        
        builder.Property(e => e.InstallationDate)
            .IsRequired()
            .HasColumnName("installation_date");
        
        builder.Property(e => e.CreatedAt)
            .IsRequired()
            .HasColumnName("created_at");
        
        builder.Property(e => e.UpdatedAt)
            .HasColumnName("updated_at");
        
        builder.HasIndex(e => e.SerialNumber)
            .IsUnique()
            .HasDatabaseName("ix_equipment_serial_number");
    }
}