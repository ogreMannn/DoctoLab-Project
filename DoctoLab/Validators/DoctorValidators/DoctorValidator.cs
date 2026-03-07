using DoctoLab.DTOs;
using FluentValidation;

namespace DoctoLab.Validators.DoctorValidators
{
    public class DoctorValidator : AbstractValidator<DoctorCreateDto>
    {
        public DoctorValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Surname)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Age)
                .InclusiveBetween(25, 70);

            RuleFor(x => x.Description)
                .MaximumLength(500);

            RuleFor(x => x.FieldId)
                .NotEmpty();

            RuleFor(x => x.HospitalId)
                .NotEmpty();

        }
    }
}
