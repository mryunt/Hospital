using Hospital.Business.Abstract;
using Hospital.DAL.Context;
using Hospital.DAL.Dtos.Disease;
using Hospital.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Business.Concrete
{
    public class DiseaseService : IDiseaseService
    {
        private readonly HospitalDbContext _hospitalDbContext;
        public DiseaseService(HospitalDbContext hospitalDbContext)
        {
            _hospitalDbContext = hospitalDbContext;
        }
        public async Task<List<GetListDiseaseDto>> GetDiseaseList()
        {
            return await _hospitalDbContext.Diseases.Include(p => p.DoctorFK).Include(p => p.PatientFK)
                .Where(p => !p.IsDeleted).Select(p => new GetListDiseaseDto
            {
                Id = p.Id,
                DoctorId = p.DoctorFK.Id,
                DoctorName = p.DoctorFK.Name,
                DoctorSurname = p.DoctorFK.Surname,
                PatientId = p.PatientFK.Id,
                PatientName = p.PatientFK.Name,
                PatientSurname = p.PatientFK.Surname
            }).ToListAsync();
        }
        public async Task<GetDiseaseDto> GetDiseaseById(int id)
        {
            return await _hospitalDbContext.Diseases.Include(p => p.DoctorFK).Include(p => p.PatientFK).Where(p => !p.IsDeleted && p.Id == id).Select(p => new GetDiseaseDto
            {
                Id = p.Id,
                DoctorId = p.DoctorFK.Id,
                DoctorName = p.DoctorFK.Name,
                DoctorSurname = p.DoctorFK.Surname,
                PatientId = p.PatientFK.Id,
                PatientName = p.PatientFK.Name,
                PatientSurname = p.PatientFK.Surname
            }).FirstOrDefaultAsync();
        }
        public async Task<int> AddDisease(AddDiseaseDto addDisease)
        {
            var newDisease = new Disease
            {
                DoctorId = addDisease.DoctorId,
                PatientId = addDisease.PatientId
            };
            await _hospitalDbContext.Diseases.AddAsync(newDisease);
            return await _hospitalDbContext.SaveChangesAsync();
        }
        public async Task<int> UpdateDisease(int id, UpdateDiseaseDto updateDisease)
        {
            var currentDisease = await _hospitalDbContext.Diseases.Where(p => !p.IsDeleted && p.Id == id).FirstOrDefaultAsync();
            if (currentDisease != null)
            {
                currentDisease.PatientId = updateDisease.PatientId;
                currentDisease.DoctorId = updateDisease.DoctorId;
                currentDisease.MDate = DateTime.Now;
                _hospitalDbContext.Diseases.Update(currentDisease);
                return await _hospitalDbContext.SaveChangesAsync();
            }
            return -1;
        }
        public async Task<int> DeleteDisease(int id)
        {
            var currentDisease = await _hospitalDbContext.Diseases.Where(p => !p.IsDeleted && p.Id == id).FirstOrDefaultAsync();
            if (currentDisease != null)
            {
                currentDisease.IsDeleted = true;
                return await _hospitalDbContext.SaveChangesAsync();
            }
            return -1;
        }
    }
}
