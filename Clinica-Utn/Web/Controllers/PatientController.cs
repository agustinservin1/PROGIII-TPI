using Application.Interfaces;
using Application.Models.Request;
using Application.Services;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PatientController : ControllerBase
    {
        private readonly IPatientService _service;

        public PatientController(IPatientService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public IActionResult GetWithAddress(int id)
        {
            var result = _service.GetPatientByIdWithAddress(id);
            return Ok(result);
        }

        [HttpGet("GetAll")]
        public IActionResult GetAllPatientsWithAddress()
        {
            var result = _service.GetPatientsWithAddress();
            return Ok(result);
        }

        [HttpPost("AddPatient")]
        [Authorize(Policy = "Admin")]
        public IActionResult AddPatient([FromBody] PatientCreateRequest request)
        {
            var newObj = _service.CreatePatient(request);

            return CreatedAtAction(nameof(GetWithAddress), new { id = newObj.Id }, newObj);
        }

        [HttpPut("Update/{id}")]
        [Authorize(Policy = "Patient")]
        public IActionResult UpdatePatient(int id, [FromBody] PatientUpdateForRequest request)
        {
            _service.UpdatePatient(id, request);
            return NoContent();
        }

        [HttpDelete("Delete/{id}")]
        [Authorize(Policy = "Admin")]
        public IActionResult DeletePatient(int id)
        {
            _service.DeletePatient(id);
            return NoContent();
        }
    }
}
