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
        [HttpGet()]
        public IActionResult GetAllPatientsWithAddress()
        {
            var result = _service.GetPatientsWithAddress();
            return Ok(result);
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
            _service.UpdatePatient(id, request);
            return NoContent();
        }

        [HttpDelete("/{id}")]

        public IActionResult DeletePatient(int id)
        {
            _service.DeletePatient(id);
            return NoContent();
        }

    }
}
