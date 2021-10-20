using FluentValidation;
using Hospital.DAL.Dtos.Doctor;

namespace Hospital.Business.Validation.Doctor
{
    public class DoctorAddValidator : AbstractValidator<AddDoctorDto>
    {
        public DoctorAddValidator()
        {
            RuleFor(p => p.Name).NotEmpty().MaximumLength(20).WithMessage("Doktor İsmi Boş Bırakılamaz ve Yirmi Karakterden Fazla Girilemez.");
            RuleFor(p => p.Surname).NotEmpty().MaximumLength(20).WithMessage("Doktor Soyismi Boş Bırakılamaz ve Yirmi Karakterden Fazla Girilemez.");
            RuleFor(p => p.Seniority).NotEmpty().WithMessage("Doktorun Ünvanı Kısmı Boş Bırakılamaz.");
            RuleFor(p => p.Departmen).NotEmpty().WithMessage("Doktorun Bölümü Kısmı Boş Bırakılamaz.");
        }
    }
}
