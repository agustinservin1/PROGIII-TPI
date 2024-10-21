using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Domain.Entities;
using Domain.Enums;
using Domain.InterFaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPatientRepository _patientRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository, IDoctorRepository doctorRepository, IPatientRepository patientRepository)
        {
            _appointmentRepository = appointmentRepository;
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
        }

        public AppointmentDto GetById(int id)
        {
            var appointment = _appointmentRepository.GetAppointmentByWithPatientAndDoctor(id);

            return AppointmentDto.CreateDto(appointment);
        }

        public IEnumerable<AppointmentDto> GetAllByDoctorId(int id)
        {
            var listAppointments = _appointmentRepository.GetAppointmentByDoctorId(id);
            return AppointmentDto.CreateList(listAppointments);
        }

        public IEnumerable<AppointmentDto> GetByDoctorAndDate(int idDoctor, DateTime date)
        {
            var list = _appointmentRepository.GetAvailableAppointmentsByDoctorAndDate(idDoctor, date);
            return AppointmentDto.CreateList(list);
        }

        public void GenerateAppointmentForDoctor(int doctorId, DateRangeRequest Date)
        {
            var doctor = _doctorRepository.GetById(doctorId);
            if (doctor == null)
            {
                throw new Exception("Doctor no encontrado.");
            }

            for (var date = Date.StartDate; date <= Date.EndDate; date = date.AddDays(1)) // Iterar sobre los días
            {
                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                {
                    continue; // Saltar sábados y domingos
                }


                for (var time = new TimeSpan(9, 0, 0); time < new TimeSpan(12, 0, 0); time = time.Add(new TimeSpan(1, 0, 0))) // Iterar de 9:00 a 12:00
                {
                    var appointment = new Appointment
                    {
                        DoctorId = doctorId,
                        Date = date.Date,
                        Time = time,
                        Status = AppointmentStatus.Available // Precarga como disponible
                    };
                    _appointmentRepository.Create(appointment);
                }


                for (var time = new TimeSpan(14, 0, 0); time < new TimeSpan(18, 0, 0); time = time.Add(new TimeSpan(1, 0, 0))) // Iterar de 14:00 a 18:00
                {
                    var appointment = new Appointment
                    {
                        DoctorId = doctorId,
                        Date = date.Date,
                        Time = time,
                        Status = AppointmentStatus.Available, // Precarga como disponible
                        PatientId = null
                    };
                    _appointmentRepository.Create(appointment);
                }
            }
        }
    }
}
