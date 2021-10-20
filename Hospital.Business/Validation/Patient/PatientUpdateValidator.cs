using FluentValidation;
using Hospital.DAL.Dtos.Patient;

namespace Hospital.Business.Validation.Patient
{
    public class PatientUpdateValidator : AbstractValidator<UpdatePatientDto>
    {
        public PatientUpdateValidator()
        {
            RuleFor(p => p.TcNo).NotEmpty().WithMessage("Hastanın Kimlik Numarası Alanı Boş Bırakılamaz.").Length(11, 11).WithMessage("tc numaranız 11 hane değil!");
            RuleFor(p => p.Name).NotEmpty().MaximumLength(20).WithMessage("Hastanın İsmi Alanı Boş Bırakılamaz ve 20 Karakteri Geçemez.");
            RuleFor(p => p.Surname).NotEmpty().MaximumLength(20).WithMessage("Hastanın Soyismi Alanı Boş Bırakılamaz ve 20 Karakteri Geçemez.");
            RuleFor(p => p.HealthInsurance).NotEmpty().WithMessage("Hastanın Sigorta Bilgisi Alanı Boş Bırakılamaz.");
        }
    }
}
