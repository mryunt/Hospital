using Hospital.Business.Abstract;
using Hospital.Business.Validation.Patient;
using Hospital.DAL.Dtos.Patient;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hospital.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;
        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }
        [HttpGet("GetPatientList")]
        public async Task<ActionResult<List<GetListPatientDto>>> GetPatientList()
        {
            try
            {
                return Ok(await _patientService.GetPatientList());
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetPatientById/{id}")]
        public async Task<ActionResult<GetPatientDto>> GetPatientById(int id)
        {
            var list = new List<string>();
            if (id <= 0)
            {
                list.Add("Hasta ID' si geçersiz!");
                return Ok(new { code = StatusCode(1001), message = list, type = "error" });
            }
            try
            {
                var currentPatient = await _patientService.GetPatientById(id);
                if (currentPatient == null)
                {
                    list.Add("Hasta Bulunamadı!");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
                else
                {
                    return currentPatient;
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPost("AddPatient")]
        public async Task<ActionResult<string>> AddPatient(AddPatientDto addPatientDto)
        {
            var list = new List<string>();
            var validator = new PatientAddValidator(_patientService);
            var validationResults = validator.Validate(addPatientDto);
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
                var result = await _patientService.AddPatient(addPatientDto);
                if (result > 0)
                {
                    list.Add("Kayıt Başarılı!");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                }
                else if (result == -1)
                {
                    list.Add("Böyle bir tc ye ait kişi vardır. Ekleme Başarısız!");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
                else
                {
                    list.Add("Kayıt Başarısız!");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPut("UpdatePatient/{id}")]
        public async Task<ActionResult<string>> UpdatePatient(int id, UpdatePatientDto updatePatientDto)
        {
            var list = new List<string>();
            var validator = new PatientUpdateValidator();
            var validationResults = validator.Validate(updatePatientDto);
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
                var result = await _patientService.UpdatePatient(id, updatePatientDto);
                if (result > 0)
                {
                    list.Add("Güncelleme İşlemi Başarılı!");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                }
                else if (result == -1)
                {
                    list.Add("Hasta Bilgisi Bulunamadı!");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
                else
                {
                    list.Add("Güncelleme İşlemi Başarısız!");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("DeletePatient/{id}")]
        public async Task<ActionResult<string>> DeletePatient(int id)
        {
            var list = new List<string>();
            try
            {
                var result = await _patientService.DeletePatient(id);
                if (result > 0)
                {
                    list.Add("Silme İşlemi Başarılı!");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                }
                else if (result == -1)
                {
                    list.Add("Hasta Bilgisi Bulunamadı!");
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
