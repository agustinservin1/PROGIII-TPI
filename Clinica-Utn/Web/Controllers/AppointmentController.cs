using Application.Interfaces;
using Application.Models.Request;
using Application.Services;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
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
            var appointment = _appointmentService.GetById(id);
            return Ok(appointment);
        }

        [HttpGet("GetByDoctorId/{doctorId}")]
        public IActionResult GetAllByDoctorId(int doctorId)
        {
            var appointment = _appointmentService.GetAllByDoctorId(doctorId);
            return Ok(appointment);
        }

        [HttpGet("GetByPatientId/{patientId}")]
        public IActionResult GetAllByPatientId(int patientId)
        {
            var appointment = _appointmentService.GetAllByPatientId(patientId);
            return Ok(appointment);
        }    
        
        [HttpGet("GetAvailableByDoctorId")]
        public IActionResult GetAppointmentsAvailable(int id) 
        {
            var appointment = _appointmentService.GetAppointmentsAvailable(id);
            return Ok(appointment);
        }

        [HttpPost("GenerateAppointments/{doctorId}")]
        [Authorize(Policy = "Doctor")]
        public IActionResult GenerateAppointments(int doctorId, [FromBody] DateRangeRequest dateRange)
        {
            _appointmentService.GenerateAppointmentForDoctor(doctorId, dateRange);
            return Ok("Turnos generados con éxito.");
        }

        [HttpPost("CreateAppointment")]
        [Authorize(Policy = "Doctor")]
        public IActionResult CreateAppointment(AppointmentCreateRequest appointmentCreateRequest)
        {
            var appointment = _appointmentService.CreateAppointment(appointmentCreateRequest);
            return Ok(appointment);
        }

        [HttpPut("AssignAppointment")]
        [Authorize(Policy = "Patient")]
        public IActionResult AssignAppointment([FromBody] AppointmentAssignForRequest appointmentAssign)
        {
            return Ok(_appointmentService.AssignAppointment(appointmentAssign));
        }

        [HttpPut("Cancel/{id}")]
        public IActionResult UpdateAppointment(int id)
        {
            var appointment = _appointmentService.CancelAppointment(id);
            return Ok(appointment);
        }

        [HttpDelete("Delete/{id}")]
        [Authorize(Policy = "Admin")]
        public IActionResult DeleteAppointment(int id)
        {
                _appointmentService.DeleteAppointment(id);
                return NoContent();
        }
    }
}
