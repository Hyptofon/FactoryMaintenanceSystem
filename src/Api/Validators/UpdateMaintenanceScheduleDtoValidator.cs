using Api.DTOs.MaintenanceSchedule;
using FluentValidation;

namespace Api.Validators;

public class UpdateMaintenanceScheduleDtoValidator : AbstractValidator<UpdateMaintenanceScheduleDto>
{
    public UpdateMaintenanceScheduleDtoValidator()
    {
        RuleFor(x => x.TaskName)
            .NotEmpty().WithMessage("Task name is required")
            .MaximumLength(100).WithMessage("Task name must not exceed 100 characters");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required")
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters");

        RuleFor(x => x.Frequency)
            .NotEmpty().WithMessage("Frequency is required")
            .Must(BeValidFrequency).WithMessage("Frequency must be one of: Daily, Weekly, Monthly, Quarterly, Annually");

        RuleFor(x => x.NextDueDate)
            .GreaterThan(DateTime.UtcNow).WithMessage("Next due date must be in the future");
    }

    private bool BeValidFrequency(string frequency)
    {
        return frequency is "Daily" or "Weekly" or "Monthly" or "Quarterly" or "Annually";
    }
}