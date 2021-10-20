using Hospital.Business.Abstract;
using Hospital.Business.Validation.Prescriptions;
using Hospital.DAL.Dtos.Prescriptions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hospital.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionsController : ControllerBase
    {
        private readonly IPrescriptionsService _prescriptionsService;
        public PrescriptionsController(IPrescriptionsService prescriptionsService)
        {
            _prescriptionsService = prescriptionsService;
        }
        [HttpGet("GetPrescriptionsList")]
        public async Task<ActionResult<List<GetListPrescriptionsDto>>> GetPrescriptionsList()
        {
            try
            {
                return Ok(await _prescriptionsService.GetPrescriptionsList());
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetPrescriptionsById/{id}")]
        public async Task<ActionResult<GetPrescriptionsDto>> GetPrescriptionsById(int id)
        {
            var list = new List<string>();
            if (id <= 0)
            {
                list.Add("Geçersiz ID!");
                return Ok(new { code = StatusCode(1002), message = list, type = "error" });
            }
            try
            {
                var currentPrescriptions = await _prescriptionsService.GetPrescriptionsById(id);
                if (currentPrescriptions == null)
                {
                    list.Add("Reçete Bilgisi Bulunamadı!");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
                else
                {
                    return currentPrescriptions;
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPost("AddPrescriptions")]
        public async Task<ActionResult<string>> AddPrescriptions(AddPrescriptionsDto addPrescriptionsDto)
        {
            var list = new List<string>();
            var validator = new PrescriptionsAddValidator();
            var validationResults = validator.Validate(addPrescriptionsDto);
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
                var result = await _prescriptionsService.AddPrescriptions(addPrescriptionsDto);
                if (result > 0)
                {
                    list.Add("Kayıt Başarılı!");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });
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
        [HttpPut("UpdatePrescriptions/{id}")]
        public async Task<ActionResult<string>> UpdatePrescriptions(int id, UpdatePrescriptionsDto updatePrescriptionsDto)
        {
            var list = new List<string>();
            var validator = new PrescriptionsUpdateValidator();
            var validationResults = validator.Validate(updatePrescriptionsDto);
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
                var result = await _prescriptionsService.UpdatePrescriptions(id, updatePrescriptionsDto);
                if (result > 0)
                {
                    list.Add("Güncelleme İşlemi Başarılı!");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                }
                else if (result == -1)
                {
                    list.Add("Reçete Kaydı Bulunamadı!");
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
        [HttpDelete("DeletePrescriptions/{id}")]
        public async Task<ActionResult<string>> DeletePrescriptions(int id)
        {
            var list = new List<string>();
            try
            {
                var result = await _prescriptionsService.DeletePrescriptions(id);
                if (result > 0)
                {
                    list.Add("Silme İşlemi Başarılı!");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                }
                else if (result == -1)
                {
                    list.Add("Reçete Kaydı Bulunamadı!");
                    return Ok(new { code = StatusCode(1002), message = list, type = "error" });
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
