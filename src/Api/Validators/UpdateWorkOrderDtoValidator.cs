using Api.DTOs.WorkOrder;
using FluentValidation;

namespace Api.Validators;

public class UpdateWorkOrderDtoValidator : AbstractValidator<UpdateWorkOrderDto>
{
    public UpdateWorkOrderDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required")
            .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters");

        RuleFor(x => x.Priority)
            .NotEmpty().WithMessage("Priority is required")
            .Must(BeValidPriority).WithMessage("Priority must be one of: Low, Medium, High, Critical");
    }

    private bool BeValidPriority(string priority)
    {
        return priority is "Low" or "Medium" or "High" or "Critical";
    }
}