using FluentValidation;
using Hospital.DAL.Dtos.Doctor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Business.Validation.Doctor
{
    public class DoctorUpdateValidator : AbstractValidator<UpdateDoctorDto>
    {
        public DoctorUpdateValidator()
        {
            RuleFor(p => p.Name).NotEmpty().MaximumLength(20).WithMessage("Doktorun İsmi Alanı Boş Bırakılamaz ve Yirmi Karakterden Fazla Girilemez.");
            RuleFor(p => p.Surname).NotEmpty().MaximumLength(20).WithMessage("Doktorun Soyismi Alanı Boş Bırakılamaz ve Yirmi Karakterden Fazla Girilemez.");
            RuleFor(p => p.Seniority).NotEmpty().WithMessage("Doktorun Kıdemi Alanı Boş Bırakılamaz.");
            RuleFor(p => p.Departmen).NotEmpty().WithMessage("Doktorun Bölümü Alanı Boş Bırakılamaz.");
        }
    }
}
