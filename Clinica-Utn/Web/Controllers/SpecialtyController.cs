using Application.Interfaces;
using Application.Models.Request;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialtyController : ControllerBase
    {
        private readonly ISpecialtyService _service;

        public SpecialtyController(ISpecialtyService service)
        {
            _service = service;
        }

        [HttpGet("/GetById/{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var result = _service.GetSpecialtyById(id);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("/GetAllSpecialties")]
        public IActionResult GetAllSpecialities()
        {
            try
            {
                var result = _service.GetAllSpecialties();
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }    
        [HttpPost("/AddSpecialty")]
        public IActionResult AddSpecialty([FromBody] SpecialtyForRequest request)
        {   
            try
            {
                var newSpecialty = _service.CreateSpecialty(request);
                return CreatedAtAction(nameof(GetById), new { id = newSpecialty.Id }, newSpecialty);

            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
