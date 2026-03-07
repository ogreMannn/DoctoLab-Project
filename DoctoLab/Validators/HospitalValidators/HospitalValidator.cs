using DoctoLab.GTOs;
using FluentValidation;

namespace DoctoLab.Validators.HospitalValidators
{
    public class HospitalValidator : AbstractValidator<HospitalCreateDto>
    {
        public HospitalValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Address)
                .NotEmpty()
                .MaximumLength(200);

            

        }
    }
}
