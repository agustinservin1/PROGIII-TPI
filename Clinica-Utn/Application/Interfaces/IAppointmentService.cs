using Application.Models;
using Application.Models.Request;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAppointmentService
    {
        AppointmentDto GetById(int id);
        IEnumerable<AppointmentDto> GetAllByDoctorId(int id);
        IEnumerable<AppointmentDto> GetAllByPatientId(int id);
        IEnumerable<AppointmentDto> GetByDoctorAndDate(int idDoctor, DateTime date);
        void GenerateAppointmentForDoctor(int doctorId, DateRangeRequest Date);
        AppointmentDto CreateAppointment(AppointmentCreateRequest appointment);
        AppointmentDto CancelAppointment(int IdAppointment);
        AppointmentDto AssignAppointment(AppointmentAssignForRequest appointmentAssign);
        AppointmentDto DeleteAppointment(int IdAppointment);
    }
}
