using FluentValidation;
using Hospital.DAL.Dtos.Prescriptions;

namespace Hospital.Business.Validation.Prescriptions
{
    public class PrescriptionsUpdateValidator : AbstractValidator<UpdatePrescriptionsDto>
    {
        public PrescriptionsUpdateValidator()
        {
            RuleFor(p => p.PatientId).NotEmpty().WithMessage("Hastanın ID' si alanı boş bırakılamaz.");
            RuleFor(p => p.MedicineName).NotEmpty().WithMessage("İlaç İsmi alanı boş bırakılamaz");
            RuleFor(p => p.Dose).NotEmpty().WithMessage("İlaç Dozu alanı boş bırakılamaz.");
        }
    }
}
