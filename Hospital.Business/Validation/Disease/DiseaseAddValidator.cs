using FluentValidation;
using Hospital.DAL.Dtos.Disease;

namespace Hospital.Business.Validation.Disease
{
    public class DiseaseAddValidator : AbstractValidator<AddDiseaseDto>
    {
        public DiseaseAddValidator()
        {
            RuleFor(p => p.DoctorId).NotEmpty().WithMessage("Doktor ID' si Boş Bırakılamaz.");
            RuleFor(p => p.PatientId).NotEmpty().WithMessage("Hasta ID' si Boş Bırakılamaz.");
        }
    }
}
