using Application.Interfaces;
using Application.Models;
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
    }
}
