using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Persistence;

public class ApplicationDbContextInitialiser
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;

    public ApplicationDbContextInitialiser(
        ApplicationDbContext context, 
        ILogger<ApplicationDbContextInitialiser> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            // Apply migrations
            await _context.Database.MigrateAsync();
            _logger.LogInformation("Database migrated successfully");

            // Seed data
            await SeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initializing the database");
            throw;
        }
    }

    private async Task SeedAsync()
    {
        // Check if data already exists
        if (await _context.Equipment.AnyAsync())
        {
            _logger.LogInformation("Database already seeded");
            return;
        }

        _logger.LogInformation("Seeding database...");

        // Seed Equipment
        var lathe = Equipment.New(
            Guid.NewGuid(),
            "Industrial Lathe",
            "LT-2000",
            "LT2000-001",
            "Workshop A",
            DateTime.UtcNow.AddYears(-2)
        );

        var cncMachine = Equipment.New(
            Guid.NewGuid(),
            "CNC Machine",
            "CNC-500X",
            "CNC500X-001",
            "Production Floor",
            DateTime.UtcNow.AddYears(-1)
        );

        var hydraulicPress = Equipment.New(
            Guid.NewGuid(),
            "Hydraulic Press",
            "HP-1000",
            "HP1000-001",
            "Workshop B",
            DateTime.UtcNow.AddMonths(-6)
        );

        await _context.Equipment.AddRangeAsync(lathe, cncMachine, hydraulicPress);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Equipment seeded: 3 items");

        // Seed Maintenance Schedules
        var schedules = new List<MaintenanceSchedule>
        {
            MaintenanceSchedule.New(
                Guid.NewGuid(),
                lathe.Id,
                "Monthly Inspection",
                "Regular monthly inspection of all components",
                MaintenanceFrequency.Monthly,
                DateTime.UtcNow.AddDays(15)
            ),
            MaintenanceSchedule.New(
                Guid.NewGuid(),
                lathe.Id,
                "Quarterly Deep Cleaning",
                "Thorough cleaning and lubrication of all parts",
                MaintenanceFrequency.Quarterly,
                DateTime.UtcNow.AddMonths(2)
            ),
            MaintenanceSchedule.New(
                Guid.NewGuid(),
                lathe.Id,
                "Annual Calibration",
                "Full calibration and precision testing",
                MaintenanceFrequency.Annually,
                DateTime.UtcNow.AddMonths(10)
            ),
            MaintenanceSchedule.New(
                Guid.NewGuid(),
                cncMachine.Id,
                "Monthly Inspection",
                "Check CNC control system and mechanical parts",
                MaintenanceFrequency.Monthly,
                DateTime.UtcNow.AddDays(20)
            ),
            MaintenanceSchedule.New(
                Guid.NewGuid(),
                cncMachine.Id,
                "Quarterly Deep Cleaning",
                "Clean spindle, tool changer, and chip conveyor",
                MaintenanceFrequency.Quarterly,
                DateTime.UtcNow.AddMonths(3)
            ),
            MaintenanceSchedule.New(
                Guid.NewGuid(),
                cncMachine.Id,
                "Annual Calibration",
                "Precision calibration and accuracy verification",
                MaintenanceFrequency.Annually,
                DateTime.UtcNow.AddMonths(11)
            ),
            MaintenanceSchedule.New(
                Guid.NewGuid(),
                hydraulicPress.Id,
                "Monthly Inspection",
                "Inspect hydraulic system and pressure gauges",
                MaintenanceFrequency.Monthly,
                DateTime.UtcNow.AddDays(10)
            ),
            MaintenanceSchedule.New(
                Guid.NewGuid(),
                hydraulicPress.Id,
                "Quarterly Deep Cleaning",
                "Replace hydraulic fluid and clean filters",
                MaintenanceFrequency.Quarterly,
                DateTime.UtcNow.AddMonths(2).AddDays(15)
            ),
            MaintenanceSchedule.New(
                Guid.NewGuid(),
                hydraulicPress.Id,
                "Annual Calibration",
                "Calibrate pressure sensors and safety systems",
                MaintenanceFrequency.Annually,
                DateTime.UtcNow.AddMonths(6)
            )
        };

        await _context.MaintenanceSchedules.AddRangeAsync(schedules);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Maintenance schedules seeded: 9 items");

        // Seed Work Orders
        var workOrders = new List<WorkOrder>
        {
            WorkOrder.New(
                Guid.NewGuid(),
                "WO-2025-001",
                lathe.Id,
                "Replace worn cutting tool",
                "The main cutting tool shows significant wear and needs replacement to maintain precision",
                WorkOrderPriority.High,
                DateTime.UtcNow.AddDays(2)
            ),
            WorkOrder.New(
                Guid.NewGuid(),
                "WO-2025-002",
                cncMachine.Id,
                "Software update",
                "Update CNC control software to latest version",
                WorkOrderPriority.Medium,
                DateTime.UtcNow.AddDays(5)
            ),
            WorkOrder.New(
                Guid.NewGuid(),
                "WO-2025-003",
                hydraulicPress.Id,
                "Hydraulic leak repair",
                "Small hydraulic fluid leak detected at main cylinder connection",
                WorkOrderPriority.Critical,
                DateTime.UtcNow.AddDays(1)
            )
        };

        // Start one work order
        workOrders[1].StartWork();

        // Complete one work order
        var completedWorkOrder = WorkOrder.New(
            Guid.NewGuid(),
            "WO-2025-000",
            lathe.Id,
            "Routine lubrication",
            "Apply lubricant to all moving parts as per maintenance schedule",
            WorkOrderPriority.Low,
            DateTime.UtcNow.AddDays(-5)
        );
        completedWorkOrder.StartWork();
        completedWorkOrder.Complete("Lubrication completed successfully. All moving parts now operating smoothly.");
        
        workOrders.Add(completedWorkOrder);

        await _context.WorkOrders.AddRangeAsync(workOrders);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Work orders seeded: 4 items (1 open, 1 in progress, 1 completed, 1 critical)");
        _logger.LogInformation("Database seeding completed successfully");
    }
}