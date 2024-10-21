using Application.Interfaces;
using Application.Models.Request;
using Domain.Exceptions;
using Domain.InterFaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
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

        [HttpGet("/GetDoctorWithSpecialty/{id}")]
        public IActionResult GetWithSpecialty(int id)
        {
            try
            {
                var result = _doctorService.GetBySpecialty(id);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("/GetAll")]
        public IActionResult GetAllDoctors()
        {
            try
            {
                var doctors = _doctorService.GetAll();
                return Ok(doctors);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        public IActionResult AddDoctor([FromBody] DoctorCreateRequest request)
        {
            var newObj = _doctorService.CreateDoctor(request);
            return CreatedAtAction(nameof(GetWithSpecialty), new { id = newObj.Id }, newObj);
        }
        [HttpPut("/UpdateDoctor/{id}")]
        public IActionResult UpdateDoctor(int id, [FromBody] DoctorUpdateRequest request)
        {
            try
            {
                _doctorService.UpdateDoctor(id, request);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("/DeleteDoctor/{id}")]

        public IActionResult DeleteDoctor(int id)
        {
            try
            {
                _doctorService.DeleteDoctor(id);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
