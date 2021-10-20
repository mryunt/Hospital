using Hospital.DAL.Dtos.Disease;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hospital.Business.Abstract
{
    public interface IDiseaseService
    {
        public Task<List<GetListDiseaseDto>> GetDiseaseList();
        public Task<GetDiseaseDto> GetDiseaseById(int id);
        public Task<int> AddDisease(AddDiseaseDto addDisease);
        public Task<int> UpdateDisease(int id, UpdateDiseaseDto updateDisease);
        public Task<int> DeleteDisease(int id);
    }
}
