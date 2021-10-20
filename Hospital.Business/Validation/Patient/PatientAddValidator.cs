using FluentValidation;
using Hospital.Business.Abstract;
using Hospital.DAL.Dtos.Patient;
using System.Threading.Tasks;

namespace Hospital.Business.Validation.Patient
{
    public class PatientAddValidator : AbstractValidator<AddPatientDto>
    {
        private readonly IPatientService _patientService;
        public PatientAddValidator(IPatientService patientService)
        {
            _patientService = patientService;
            RuleFor(p => p.TcNo).NotEmpty().WithMessage("Hastanın Kimlik Numarası Alanı Boş Bırakılamaz.")
                .Must(AnyTCNumber).WithMessage("böyle tc ye ait kişi vardır.")
                .Length(11,11).WithMessage("tc numaranız 11 hane değil!");
            RuleFor(p => p.Name).NotEmpty().MaximumLength(20).WithMessage("Hastanın İsmi Alanı Boş Bırakılamaz ve 20 Karakteri Geçemez.");
            RuleFor(p => p.Surname).NotEmpty().MaximumLength(20).WithMessage("Hastanın Soyismi Alanı Boş Bırakılamaz ve 2o Karakteri Geçemez.");
            RuleFor(p => p.HealthInsurance).NotEmpty().WithMessage("Hastanın Sigorta Bilgisi Alanı Boş Bırakılamaz.");
        }

        public bool AnyTCNumber(string tcNumber)
        {
            return !_patientService.AnyTCNumber(tcNumber);
        }
    }
}
