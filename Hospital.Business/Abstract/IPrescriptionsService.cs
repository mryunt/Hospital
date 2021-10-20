using Hospital.DAL.Dtos.Prescriptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Business.Abstract
{
    public interface IPrescriptionsService
    {
        public Task<List<GetListPrescriptionsDto>> GetPrescriptionsList();
        public Task<GetPrescriptionsDto> GetPrescriptionsById(int id);
        public Task<int> AddPrescriptions(AddPrescriptionsDto addPrescriptions);
        public Task<int> UpdatePrescriptions(int id, UpdatePrescriptionsDto updatePrescriptions);
        public Task<int> DeletePrescriptions(int id);
    }
}
