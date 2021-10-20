using Hospital.DAL.Dtos.Patient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hospital.Business.Abstract
{
    public interface IPatientService
    {
        Task<List<GetListPatientDto>> GetPatientList();
        Task<GetPatientDto> GetPatientById(int id);
        Task<int> AddPatient(AddPatientDto addPatient);
        Task<int> UpdatePatient(int id, UpdatePatientDto updatePatient);
        Task<int> DeletePatient(int id);

        bool AnyTCNumber(string tcNumber);
    }
}
