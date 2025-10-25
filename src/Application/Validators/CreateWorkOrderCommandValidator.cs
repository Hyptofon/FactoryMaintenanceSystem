using Application.WorkOrders.Commands;
using FluentValidation;

namespace Application.Validators;

public class CreateWorkOrderCommandValidator : AbstractValidator<CreateWorkOrderCommand>
{
    public CreateWorkOrderCommandValidator()
    {
        RuleFor(x => x.WorkOrderNumber)
            .NotEmpty().WithMessage("Work order number is required");

        RuleFor(x => x.EquipmentId)
            .NotEmpty().WithMessage("Equipment ID is required");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required")
            .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters");
        
        RuleFor(x => x.ScheduledDate)
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("Scheduled date must be in the future");
    }
}