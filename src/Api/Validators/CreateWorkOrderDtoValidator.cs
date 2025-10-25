using Api.DTOs.WorkOrder;
using FluentValidation;

namespace Api.Validators;

public class CreateWorkOrderDtoValidator : AbstractValidator<CreateWorkOrderDto>
{
    public CreateWorkOrderDtoValidator()
    {
        RuleFor(x => x.WorkOrderNumber)
            .NotEmpty().WithMessage("Work order number is required")
            .MaximumLength(50).WithMessage("Work order number must not exceed 50 characters");

        RuleFor(x => x.EquipmentId)
            .NotEmpty().WithMessage("Equipment ID is required");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required")
            .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters");

        RuleFor(x => x.Priority)
            .NotEmpty().WithMessage("Priority is required")
            .Must(BeValidPriority).WithMessage("Priority must be one of: Low, Medium, High, Critical");
        
        RuleFor(x => x.ScheduledDate)
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("Scheduled date must be in the future for new orders");
    }

    private bool BeValidPriority(string priority)
    {
        return priority is "Low" or "Medium" or "High" or "Critical";
    }
}