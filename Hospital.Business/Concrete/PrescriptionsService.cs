using Hospital.Business.Abstract;
using Hospital.DAL.Context;
using Hospital.DAL.Dtos.Prescriptions;
using Hospital.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Business.Concrete
{
    public class PrescriptionsService : IPrescriptionsService
    {
        private readonly HospitalDbContext _hospitalDbContext;
        public PrescriptionsService(HospitalDbContext hospitalDbContext)
        {
            _hospitalDbContext = hospitalDbContext;
        }
        public async Task<List<GetListPrescriptionsDto>> GetPrescriptionsList()
        {
            return await _hospitalDbContext.Prescriptionses.Include(p => p.PatientFK)
                .Where(p => !p.IsDeleted).Select(p => new GetListPrescriptionsDto
            {
                Id = p.Id,
                MedicineName = p.MedicineName,
                Dose = p.Dose,
                PatientId = p.PatientFK.Id,
                PatientName = p.PatientFK.Name,
                PatientSurname = p.PatientFK.Surname
            }).ToListAsync();
        }
        public async Task<GetPrescriptionsDto> GetPrescriptionsById(int id)
        {
            return await _hospitalDbContext.Prescriptionses.Include(p => p.PatientFK)
                .Where(p => !p.IsDeleted && p.Id == id).Select(p => new GetPrescriptionsDto
            {
                Id = p.Id,
                MedicineName = p.MedicineName,
                Dose = p.Dose,
                PatientId = p.PatientFK.Id,
                PatientName = p.PatientFK.Name,
                PatientSurname = p.PatientFK.Surname
            }).FirstOrDefaultAsync();
        }
        public async Task<int> AddPrescriptions(AddPrescriptionsDto addPrescriptions)
        {
            var newPrescriptions = new Prescriptions
            {
                PatientId = addPrescriptions.PatientId,
                MedicineName = addPrescriptions.MedicineName,
                Dose = addPrescriptions.Dose
            };
            await _hospitalDbContext.Prescriptionses.AddAsync(newPrescriptions);
            return await _hospitalDbContext.SaveChangesAsync();
        }
        public async Task<int> UpdatePrescriptions(int id, UpdatePrescriptionsDto updatePrescriptions)
        {
            var currentPrescriptions = await _hospitalDbContext.Prescriptionses.Where(p => !p.IsDeleted && p.Id == id).FirstOrDefaultAsync();
            if (currentPrescriptions != null)
            {
                currentPrescriptions.PatientId = updatePrescriptions.PatientId;
                currentPrescriptions.MedicineName = updatePrescriptions.MedicineName;
                currentPrescriptions.Dose = updatePrescriptions.Dose;
                currentPrescriptions.MDate = DateTime.Now;
                _hospitalDbContext.Prescriptionses.Update(currentPrescriptions);
                return await _hospitalDbContext.SaveChangesAsync();
            }
            return -1;
        }
        public async Task<int> DeletePrescriptions(int id)
        {
            var currentPrescriptions = await _hospitalDbContext.Prescriptionses.Where(p => !p.IsDeleted && p.Id == id).FirstOrDefaultAsync();
            if (currentPrescriptions != null)
            {
                currentPrescriptions.IsDeleted = true;
                return await _hospitalDbContext.SaveChangesAsync();
            }
            return -1;
        }
    }
}
