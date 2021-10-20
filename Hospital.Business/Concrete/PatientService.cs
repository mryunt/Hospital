using Hospital.Business.Abstract;
using Hospital.DAL.Context;
using Hospital.DAL.Dtos.Patient;
using Hospital.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Business.Concrete
{
    public class PatientService : IPatientService
    {
        private readonly HospitalDbContext _hospitalDbContext;
        public PatientService(HospitalDbContext hospitalDbContext)
        {
            _hospitalDbContext = hospitalDbContext;
        }
        public async Task<List<GetListPatientDto>> GetPatientList()
        {
            return await _hospitalDbContext.Patients.Where(p => !p.IsDeleted).Select(p => new GetListPatientDto
            {
                Id = p.Id,
                TcNo = p.TcNo,
                Name = p.Name,
                Surname = p.Surname,
                HealthInsurance = p.HealthInsurance
            }).ToListAsync();
        }
        public async Task<GetPatientDto> GetPatientById(int id)
        {
            return await _hospitalDbContext.Patients.Where(p => !p.IsDeleted && p.Id == id).Select(p => new GetPatientDto
            {
                Id = p.Id,
                TcNo = p.TcNo,
                Name = p.Name,
                Surname = p.Surname,
                HealthInsurance = p.HealthInsurance
            }).FirstOrDefaultAsync();
        }
        public async Task<int> AddPatient(AddPatientDto addPatient)
        {
            //var control = AnyTCNumber(addPatient.TcNo);
            //if (!control)
            //{
                var newPatient = new Patient
                {
                    TcNo = addPatient.TcNo,
                    Name = addPatient.Name,
                    Surname = addPatient.Surname,
                    HealthInsurance = addPatient.HealthInsurance
                };
                await _hospitalDbContext.Patients.AddAsync(newPatient);
                return await _hospitalDbContext.SaveChangesAsync();
            //}
            //return -1;
        }
        public async Task<int> UpdatePatient(int id, UpdatePatientDto updatePatient)
        {
            var currentPatient = await _hospitalDbContext.Patients.Where(p => !p.IsDeleted && p.Id == id).FirstOrDefaultAsync();
            if (currentPatient != null)
            {
                currentPatient.TcNo = updatePatient.TcNo;
                currentPatient.Name = updatePatient.Name;
                currentPatient.Surname = updatePatient.Surname;
                currentPatient.HealthInsurance = updatePatient.HealthInsurance;
                _hospitalDbContext.Patients.Update(currentPatient);
                return await _hospitalDbContext.SaveChangesAsync();
            }
            return -1;
        }
        public async Task<int> DeletePatient(int id)
        {
            var currentPatient = await _hospitalDbContext.Patients.Where(p => !p.IsDeleted && p.Id == id).FirstOrDefaultAsync();
            if (currentPatient != null)
            {
                currentPatient.IsDeleted = true;
                return await _hospitalDbContext.SaveChangesAsync();
            }
            return -1;
        }

        public bool AnyTCNumber(string tcNumber)
        {
            return _hospitalDbContext.Patients.Where(p => p.TcNo == tcNumber).Any();
        }
    }
}
