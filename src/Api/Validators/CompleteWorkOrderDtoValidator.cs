using Api.DTOs.WorkOrder;
using FluentValidation;

namespace Api.Validators;

public class CompleteWorkOrderDtoValidator : AbstractValidator<CompleteWorkOrderDto>
{
    public CompleteWorkOrderDtoValidator()
    {
        RuleFor(x => x.CompletionNotes)
            .NotEmpty().WithMessage("Completion notes are required")
            .MinimumLength(10).WithMessage("Completion notes must be at least 10 characters")
            .MaximumLength(1000).WithMessage("Completion notes must not exceed 1000 characters");
    }
}