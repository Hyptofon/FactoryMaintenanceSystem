using Application.MaintenanceSchedules.Commands;
using FluentValidation;

namespace Application.Validators;

public class UpdateMaintenanceScheduleCommandValidator : AbstractValidator<UpdateMaintenanceScheduleCommand>
{
    public UpdateMaintenanceScheduleCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Schedule ID is required");

        RuleFor(x => x.TaskName)
            .NotEmpty().WithMessage("Task name is required")
            .MaximumLength(100).WithMessage("Task name must not exceed 100 characters");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required")
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters");

        RuleFor(x => x.NextDueDate)
            .GreaterThan(DateTime.UtcNow).WithMessage("Next due date must be in the future");
    }
}