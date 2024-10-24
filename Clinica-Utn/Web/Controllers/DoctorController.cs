using Application.Interfaces;
using Application.Models.Request;
using Domain.Exceptions;
using Domain.InterFaces;
using Microsoft.AspNetCore.Authorization;
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
                var result = _doctorService.GetBySpecialty(id);
                return Ok(result);
        }
        [HttpGet("/GetAllDoctors")]
        public IActionResult GetAllDoctors()
        {
                var doctors = _doctorService.GetAll();
                return Ok(doctors);
        }
        [HttpPost ("/AddDoctor")]
        //[Authorize(Policy = "Admin")]
        public IActionResult AddDoctor([FromBody] DoctorCreateRequest request)
        {
            var newObj = _doctorService.CreateDoctor(request);
            return CreatedAtAction(nameof(GetWithSpecialty), new { id = newObj.Id }, newObj);
        }

        [Authorize(Policy = "Doctor")]
        [HttpPut("/UpdateDoctor/{id}")]
        public IActionResult UpdateDoctor(int id, [FromBody] DoctorUpdateRequest request)
        {
                _doctorService.UpdateDoctor(id, request);
                return NoContent();
        }

        [HttpDelete("/DeleteDoctor/{id}")]
        public IActionResult DeleteDoctor(int id)
        {
                _doctorService.DeleteDoctor(id);
                return NoContent();
        }
    }
}
