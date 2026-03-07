using DoctoLab.DTOs;
using FluentValidation;

namespace DoctoLab.Validators.AppointmentValidators
{
    public class AppointmentValidator : AbstractValidator<AppointmentCreateDto>
    {
        public AppointmentValidator()
        {
            RuleFor(x => x.DoctorId)
                .NotEmpty();

            RuleFor(x => x.PatientId)
                .NotEmpty();

            RuleFor(x => x.AppointmentDate)
                .GreaterThan(DateTime.UtcNow)
                .WithMessage("Appointment must be in the future");
        }
    }
}
