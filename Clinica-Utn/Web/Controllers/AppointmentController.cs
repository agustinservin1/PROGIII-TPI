using Application.Interfaces;
using Application.Models.Request;
using Application.Services;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        public AppointmentController(IAppointmentService service)
        {
            _appointmentService = service;
        }

        [HttpGet("/{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_appointmentService.GetById(id));
        }

        [HttpPost("generate/{doctorId}")]
        public IActionResult GenerateAppointments(int doctorId, [FromBody] DateRangeRequest dateRange)
        {
            _appointmentService.GenerateAppointmentForDoctor(doctorId, dateRange);
            return Ok("Turnos generados con éxito.");
        }

        [HttpGet("/Doctors/{doctorId}")]
        public IActionResult GetAllByDoctorId(int doctorId)
        {
            return Ok(_appointmentService.GetAllByDoctorId(doctorId));
        }

        [HttpGet("SearchByDoctorsAndDate/")]
        public IActionResult GetByDoctorAndDate([FromQuery] int doctorId, [FromQuery] DateTime date)
        {
            return Ok(_appointmentService.GetByDoctorAndDate(doctorId, date));
        }

        [HttpPost("/AssignAppointment")]
        public IActionResult AssignAppointment([FromBody] AppointmentAssignForRequest appointmentAssign)
        {
            return Ok(_appointmentService.AssignAppointment(appointmentAssign));
        }

        [HttpPut("/Cancel{id}")]
        public IActionResult UpdateAppointment(int id)
        {
            return Ok(_appointmentService.CancelAppointment(id));
        }

        [HttpDelete("/Delete{id}")]
        public IActionResult DeleteAppointment(int id)
        {
            try
            {
                var appointment = _appointmentService.DeleteAppointment(id);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
