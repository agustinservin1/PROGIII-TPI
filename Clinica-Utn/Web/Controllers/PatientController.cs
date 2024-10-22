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
            try
            {
                var result = _service.GetPatientByIdWithAddress(id);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet()]
        public IActionResult GetAllPatientsWithAddress()
        {
            try
            {
                var result = _service.GetPatientsWithAddress();
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        //[Authorize(Policy = "Patient")]
        public IActionResult AddPatient([FromBody] PatientCreateRequest request)
        {
            var newObj = _service.CreatePatient(request);

            return CreatedAtAction(nameof(GetWithAddress), new { id = newObj.Id }, newObj);
        }

        [HttpPut("/{id}")]
        public IActionResult UpdatePatient(int id, [FromBody] PatientUpdateForRequest request)
        {
            try
            {
                _service.UpdatePatient(id, request);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("/{id}")]

        public IActionResult DeletePatient(int id)
        {
            try
            {
                _service.DeletePatient(id);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
