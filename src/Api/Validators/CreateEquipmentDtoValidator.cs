using Api.DTOs.Equipment;
using FluentValidation;

namespace Api.Validators;

public class CreateEquipmentDtoValidator : AbstractValidator<CreateEquipmentDto>
{
    public CreateEquipmentDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters");

        RuleFor(x => x.Model)
            .NotEmpty().WithMessage("Model is required")
            .MaximumLength(50).WithMessage("Model must not exceed 50 characters");

        RuleFor(x => x.SerialNumber)
            .NotEmpty().WithMessage("Serial number is required")
            .MaximumLength(50).WithMessage("Serial number must not exceed 50 characters");

        RuleFor(x => x.Location)
            .NotEmpty().WithMessage("Location is required")
            .MaximumLength(200).WithMessage("Location must not exceed 200 characters");

        RuleFor(x => x.InstallationDate)
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Installation date cannot be in the future");
    }
}