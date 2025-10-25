using Api.DTOs.Equipment;
using FluentValidation;

namespace Api.Validators;

public class UpdateEquipmentStatusDtoValidator : AbstractValidator<UpdateEquipmentStatusDto>
{
    public UpdateEquipmentStatusDtoValidator()
    {
        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Status is required")
            .Must(BeValidStatus).WithMessage("Status must be one of: Operational, UnderMaintenance, OutOfService");
    }

    private bool BeValidStatus(string status)
    {
        return status is "Operational" or "UnderMaintenance" or "OutOfService";
    }
}