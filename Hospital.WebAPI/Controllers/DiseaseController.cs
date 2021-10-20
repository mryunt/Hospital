using Hospital.Business.Abstract;
using Hospital.Business.Validation.Disease;
using Hospital.DAL.Dtos.Disease;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hospital.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiseaseController : ControllerBase
    {
        private readonly IDiseaseService _diseaseService;
        public DiseaseController(IDiseaseService diseaseService)
        {
            _diseaseService = diseaseService;
        }
        [HttpGet("GetDiseaseList")]
        public async Task<ActionResult<List<GetListDiseaseDto>>> GetDiseaseList()
        {
            try
            {
                return Ok(await _diseaseService.GetDiseaseList());
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetDiseaseById/{id}")]
        public async Task<ActionResult<GetDiseaseDto>> GetDiseaseById(int id)
        {
            var list = new List<string>();
            if (id <= 0)
            {
                list.Add("Hastalık ID' si geçersiz!");
                return Ok(new { code = StatusCode(1001), message = list, type = "error" });
            }
            try
            {
                var currentDisease = await _diseaseService.GetDiseaseById(id);
                if (currentDisease != null)
                {
                    return currentDisease;
                }
                else
                {
                    list.Add("Hastalık Bulunamadı!");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPost("AddDisease")]
        public async Task<ActionResult<string>> AddDisease(AddDiseaseDto addDiseaseDto)
        {
            var list = new List<string>();
            var validator = new DiseaseAddValidator();
            var validationResults = validator.Validate(addDiseaseDto);
            if (!validationResults.IsValid)
            {
                foreach (var error in validationResults.Errors)
                {
                    list.Add(error.ErrorMessage);
                    return Ok(new { code = StatusCode(1002), message = list, type = "error" });
                }
            }
            try
            {
                var result = await _diseaseService.AddDisease(addDiseaseDto);
                if (result > 0)
                {
                    list.Add("Kayıt İşlemi Başarılı!");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                }
                else
                {
                    list.Add("Kayıt İşlemi Başarısız!");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPut("UpdateDisease/{id}")]
        public async Task<ActionResult<string>> UpdateDisease(int id, UpdateDiseaseDto updateDiseaseDto)
        {
            var list = new List<string>();
            var validator = new DiseaseUpdateValidator();
            var validationResults = validator.Validate(updateDiseaseDto);
            if (!validationResults.IsValid)
            {
                foreach (var error in validationResults.Errors)
                {
                    list.Add(error.ErrorMessage);
                    return Ok(new { code = StatusCode(1002), message = list, type = "error" });
                }
            }
            try
            {
                var result = await _diseaseService.UpdateDisease(id, updateDiseaseDto);
                if (result > 0)
                {
                    list.Add("Güncelleme İşlemi Başarılı!");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                }
                else if (result == -1)
                {
                    list.Add("Kayıt Bulunamadı");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
                else
                {
                    list.Add("Güncelleme İşlemi Başarısız.");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("DeleteDisease/{id}")]
        public async Task<ActionResult<string>> DeleteDisease(int id)
        {
            var list = new List<string>();
            try
            {
                var result = await _diseaseService.DeleteDisease(id);
                if (result > 0)
                {
                    list.Add("Silme İşlemi Başarılı!");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                }
                else if (result == -1)
                {
                    list.Add("Kayıt Bulunamadı!");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
                else
                {
                    list.Add("Silme İşlemi Başarısız!");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
