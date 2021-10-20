using Hospital.Business.Abstract;
using Hospital.Business.Validation.Doctor;
using Hospital.DAL.Dtos.Doctor;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hospital.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }
        [HttpGet("GetDoctorList")]
        public async Task<ActionResult<List<GetListDoctorDto>>> GetDoctorList()
        {
            try
            {
                return Ok(await _doctorService.GetDoctorList());
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetDoctorById/{id}")]
        public async Task<ActionResult<GetDoctorDto>> GetDoctorById(int id)
        {
            var list = new List<string>();
            if (id <= 0)
            {
                list.Add("ID Geçersiz.");
                return Ok(new { code = StatusCode(1002), message = list, type = "error" });
            }
            try
            {
                var currentDoctor = await _doctorService.GetDoctorById(id);
                if (currentDoctor != null)
                {
                    return currentDoctor;
                }
                else
                {
                    list.Add("Doktor Bulunamadı!");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPost("AddDoctor")]
        public async Task<ActionResult<string>> AddDoctor(AddDoctorDto addDoctorDto)
        {
            var list = new List<string>();
            var validator = new DoctorAddValidator();
            var validationResult = validator.Validate(addDoctorDto);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    list.Add(error.ErrorMessage);
                    return Ok(new { code = StatusCode(1002), message = list, type = "error" });
                }
            }
            try
            {
                var result = await _doctorService.AddDoctor(addDoctorDto);
                if (result > 0)
                {
                    list.Add("Ekleme İşlemi Başarılı!");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                }
                else
                {
                    list.Add("Ekleme İşlemi Başarısız!");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPut("UpdateDoctor/{id}")]
        public async Task<ActionResult<string>> UpdateDoctor(int id, UpdateDoctorDto updateDoctorDto)
        {
            var list = new List<string>();
            var validator = new DoctorUpdateValidator();
            var validationResults = validator.Validate(updateDoctorDto);
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
                var result = await _doctorService.UpdateDoctor(id, updateDoctorDto);
                if (result > 0)
                {
                    list.Add("Güncelleme İşlemi Başarılı!");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                }
                else if (result == -1)
                {
                    list.Add("Kayıt Bulunamadı!");
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
        [HttpDelete("DeleteDoctor/{id}")]
        public async Task<ActionResult<string>> DeleteDoctor(int id)
        {
            var list = new List<string>();
            try
            {
                var result = await _doctorService.DeleteDoctor(id);
                if (result > 0)
                {
                    list.Add("Silme işlemi Başarılı!");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                }
                else if (result == -1)
                {
                    list.Add("Kayıt Bulunamadı!");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
                else
                {
                    list.Add("Silme işlemi Başarısız!");
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
