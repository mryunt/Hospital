using Hospital.Business.Abstract;
using Hospital.DAL.Context;
using Hospital.DAL.Dtos.Doctor;
using Hospital.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Business.Concrete
{
    public class DoctorService : IDoctorService
    {
        private readonly HospitalDbContext _hospitalDbContext;
        public DoctorService(HospitalDbContext hospitalDbContext)
        {
            _hospitalDbContext = hospitalDbContext;
        }
        public async Task<List<GetListDoctorDto>> GetDoctorList()
        {
            return await _hospitalDbContext.Doctors.Where(p => !p.IsDeleted).Select(p => new GetListDoctorDto
            {
                Id = p.Id,
                Name = p.Name,
                Surname = p.Surname,
                Seniority = p.Seniority,
                Departmen = p.Departmen
            }).ToListAsync();
        }
        public async Task<GetDoctorDto> GetDoctorById(int id)
        {
            return await _hospitalDbContext.Doctors.Where(p => !p.IsDeleted && p.Id == id).Select(p => new GetDoctorDto
            {
                Id = p.Id,
                Name = p.Name,
                Surname = p.Surname,
                Departmen = p.Departmen,
                Seniority = p.Seniority,
            }).FirstOrDefaultAsync();
        }
        public async Task<int> AddDoctor(AddDoctorDto addDoctor)
        {
            var newDoctor = new Doctor
            {
                Name = addDoctor.Name,
                Surname = addDoctor.Surname,
                Seniority = addDoctor.Seniority,
                Departmen = addDoctor.Departmen,
            };
            await _hospitalDbContext.Doctors.AddAsync(newDoctor);
            return await _hospitalDbContext.SaveChangesAsync();
        }
        public async Task<int> UpdateDoctor(int id, UpdateDoctorDto updateDoctor)
        {
            var currentDoctor = await _hospitalDbContext.Doctors.Where(p => !p.IsDeleted && p.Id == id).FirstOrDefaultAsync();
            if (currentDoctor != null)
            {
                currentDoctor.Name = updateDoctor.Name;
                currentDoctor.Surname = updateDoctor.Surname;
                currentDoctor.Seniority = updateDoctor.Seniority;
                currentDoctor.Departmen = updateDoctor.Departmen;
                currentDoctor.MDate = DateTime.Now;
                _hospitalDbContext.Doctors.Update(currentDoctor);
                return await _hospitalDbContext.SaveChangesAsync();
            }
            return -1;
        }
        public async Task<int> DeleteDoctor(int id)
        {
            var currentDoctor = await _hospitalDbContext.Doctors.Where(p => !p.IsDeleted && p.Id == id).FirstOrDefaultAsync();
            if (currentDoctor != null)
            {
                currentDoctor.IsDeleted = true;
                return await _hospitalDbContext.SaveChangesAsync();
            }
            return -1;
        }
    }
}
