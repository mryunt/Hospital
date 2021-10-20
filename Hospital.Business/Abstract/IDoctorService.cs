using Hospital.DAL.Dtos.Doctor;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hospital.Business.Abstract
{
    public interface IDoctorService
    {
        public Task<List<GetListDoctorDto>> GetDoctorList();
        public Task<GetDoctorDto> GetDoctorById(int id);
        public Task<int> AddDoctor(AddDoctorDto addDoctor);
        public Task<int> UpdateDoctor(int id, UpdateDoctorDto updateDoctor);
        public Task<int> DeleteDoctor(int id);
    }
}
