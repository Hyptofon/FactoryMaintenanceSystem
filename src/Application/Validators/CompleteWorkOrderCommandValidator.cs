using Application.WorkOrders.Commands;
using FluentValidation;

namespace Application.Validators;

public class CompleteWorkOrderCommandValidator : AbstractValidator<CompleteWorkOrderCommand>
{
    public CompleteWorkOrderCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Work order ID is required");

        RuleFor(x => x.CompletionNotes)
            .NotEmpty().WithMessage("Completion notes are required")
            .MaximumLength(1000).WithMessage("Completion notes must not exceed 1000 characters");
        
        
    }
}