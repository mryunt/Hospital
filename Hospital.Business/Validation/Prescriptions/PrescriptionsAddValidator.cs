using FluentValidation;
using Hospital.DAL.Dtos.Prescriptions;

namespace Hospital.Business.Validation.Prescriptions
{
    public class PrescriptionsAddValidator : AbstractValidator<AddPrescriptionsDto>
    {
        public PrescriptionsAddValidator()
        {
            RuleFor(p => p.PatientId).NotEmpty().WithMessage("Hasta ID' si Alanı Boş Bırakılamaz.");
            RuleFor(p => p.MedicineName).NotEmpty().WithMessage("İlaç İsmi Alanı Boş Bırakılamaz.");
            RuleFor(p => p.Dose).NotEmpty().WithMessage("İlaç Dozu Alanı Boş Bırakılamaz.");
        }
    }
}
