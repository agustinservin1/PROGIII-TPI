using Domain.Entities;
using Domain.Enums;
using Domain.InterFaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class AppointmentRepository : BaseRepository<Appointment>, IAppointmentRepository
    {
        private readonly ApplicationContext _repository;

        public AppointmentRepository(ApplicationContext repository) : base(repository)
        {
            _repository = repository;
        }

        public Appointment? GetAppointmentByWithPatientAndDoctor(int id)
        {
            var appointment = _repository.Appointments
                                        .Include(c => c.Patient)
                                        .Include(c => c.Doctor)
                                        .FirstOrDefault(c => c.Id == id);
            return appointment;
        }

        public IEnumerable<Appointment> GetAppointmentByPatientId(int patientId)
        {
            var appointments = _repository.Appointments
                                        .Where(a => a.PatientId == patientId)
                                        .Include(c => c.Patient)
                                        .Include(c => c.Doctor)
                                        .ToList();

            return appointments;
        }

        public IEnumerable<Appointment> GetAppointmentByDoctorId(int doctorId)
        {
            var appointments = _repository.Appointments
                                        .Where(a => a.DoctorId == doctorId)
                                        .Include(a => a.Patient)
                                        .ToList();
            return appointments;
        }

       

        public IEnumerable<Appointment> GetByAvailable(int id)
        {
            
            return _repository.Appointments.Where(a =>a.DoctorId == id && a.Status == AppointmentStatus.Available && a.Date >= DateTime.Now )
                                           .ToList();
        }
    }
}
