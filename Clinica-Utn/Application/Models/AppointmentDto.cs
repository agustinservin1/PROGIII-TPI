using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class AppointmentDto
    {
        public int IdAppointment { get; set; }
        public int DoctorId { get; set; }
        public int? PatientId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string Office { get; set; } = string.Empty;

        public static AppointmentDto CreateDto(Appointment appointment)
        {
            var newAppointmentDto = new AppointmentDto()
            {
                IdAppointment = appointment.Id,
                DoctorId = appointment.DoctorId,
                PatientId = appointment.PatientId,
                Date = appointment.Date,
                Time = appointment.Time
            };
            return newAppointmentDto;
        }

        public static IEnumerable<AppointmentDto> CreateList(IEnumerable<Appointment> list)
        {
            List<AppointmentDto> listDto = new List<AppointmentDto>();
            foreach (Appointment a in list)
            {
                listDto.Add(CreateDto(a));
            }
            return listDto;
        }
    }
}
