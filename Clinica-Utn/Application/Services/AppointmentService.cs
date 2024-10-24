using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.InterFaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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

            return appointment == null
                ? throw new NotFoundException($"No se encontró la cita con el id {id}")
                : AppointmentDto.CreateDto(appointment);
        }

        public IEnumerable<AppointmentDto> GetAllByDoctorId(int id)
        {
            var listAppointments = _appointmentRepository.GetAppointmentByDoctorId(id);
            return AppointmentDto.CreateList(listAppointments);
        }

        public IEnumerable<AppointmentDto> GetAllByPatientId(int id)
        {
            var listAppointments = _appointmentRepository.GetAppointmentByPatientId(id);
            return AppointmentDto.CreateList(listAppointments);
        }

        public IEnumerable<AppointmentDto> GetAppointmentsAvailable(int id)
        {
            var entity = _doctorRepository.GetById(id);

            if (entity ==null)
            {
                throw new NotFoundException($"No se encontró doctor con el id indicado {id}");
            }
            var listAppointments = _appointmentRepository.GetByAvailable(id);
            return AppointmentDto.CreateList(listAppointments);

        }

     
        public void GenerateAppointmentForDoctor(int doctorId, DateRangeRequest Date)
        {
            var doctor = _doctorRepository.GetById(doctorId);

            if (doctor == null)
            {
                throw new NotFoundException("Doctor no encontrado.");
            }

            var appointmentsDb = _appointmentRepository.GetAppointmentByDoctorId(doctorId);

            for (var date = Date.StartDate; date <= Date.EndDate; date = date.AddDays(1))
            {
                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                {
                    continue;
                }


                for (var time = new TimeSpan(9, 0, 0); time < new TimeSpan(12, 0, 0); time = time.Add(new TimeSpan(1, 0, 0)))
                {
                    var appointment = new AppointmentCreateRequest
                    {
                        DoctorId = doctorId,
                        Date = date.Date,
                        Time = time.ToString(@"hh\:mm\:ss"),
                        Status = AppointmentStatus.Available,
                        PatientId = null
                    };

                    CreateAppointment(appointment);
                }


                for (var time = new TimeSpan(14, 0, 0); time < new TimeSpan(18, 0, 0); time = time.Add(new TimeSpan(1, 0, 0)))
                {
                    var appointment = new AppointmentCreateRequest
                    {
                        DoctorId = doctorId,
                        Date = date.Date,
                        Time = time.ToString(@"hh\:mm\:ss"),
                        Status = AppointmentStatus.Available,
                        PatientId = null
                    };

                    CreateAppointment(appointment);
                }
            }
        }

        public AppointmentDto CreateAppointment(AppointmentCreateRequest appointment)
        {
            var appointmentsDb = _appointmentRepository.GetAppointmentByDoctorId(appointment.DoctorId);
            if (appointment.PatientId is not null) {
                var patient = _patientRepository.GetByIdIncludeAddress(appointment.PatientId.Value);
                if (patient == null)
                {
                    throw new NotFoundException("No existe paciente con el ID indicado");
                } 
            }
            
            if (!appointmentsDb.Any(a => appointment.Date == a.Date && TimeSpan.Parse(appointment.Time) == a.Time)) //Agregar validación de hora
            {
                var newAppointemnt = new Appointment()
                {
                    DoctorId = appointment.DoctorId,
                    PatientId = appointment.PatientId,
                    Time = TimeSpan.Parse(appointment.Time),
                    Date = appointment.Date,
                    Status = AppointmentStatus.Available,
                };

                _appointmentRepository.Create(newAppointemnt);

                return AppointmentDto.CreateDto(newAppointemnt);
            }

            throw new NotFoundException("Ya existe un turno en el horario ingresado.");

        }

        public AppointmentDto CancelAppointment(int IdAppointment)
        {
            var entity = _appointmentRepository.GetById(IdAppointment) ?? throw new Exception("Cita no encontrada.");

            entity.Status = AppointmentStatus.Canceled;

            _appointmentRepository.Update(entity);

            return AppointmentDto.CreateDto(entity);
        }

        public AppointmentDto AssignAppointment(AppointmentAssignForRequest appointmentAssign)
        {
            var entity = _appointmentRepository.GetById(appointmentAssign.IdAppointment);

            if (entity == null)
            {
                throw new Exception("Cita no encontrada.");
            }

            var patient = _patientRepository.GetByIdIncludeAddress(appointmentAssign.IdPatient);

            if (patient == null)
            {
                throw new Exception("Paciente no encontrado.");
            }

            if (entity.Status != AppointmentStatus.Available)
            {
                throw new NotFoundException("No esta disponible.");
            }

            var currentTime = DateTime.Now.TimeOfDay;
            

            if ((entity.Time - currentTime).TotalMinutes <= 30)
            {
                throw new NotFoundException("No se puede asignar turnos con menos de 30 minutos de anticipacion.");
            }

            entity.PatientId = appointmentAssign.IdPatient;

            entity.Status = AppointmentStatus.Reserved;

            _appointmentRepository.Update(entity);

            return AppointmentDto.CreateDto(entity);
        }

        public AppointmentDto DeleteAppointment(int IdAppointment)
        {
            var appointment = _appointmentRepository.GetById(IdAppointment);

            var entity = _appointmentRepository.Delete(appointment);

            return AppointmentDto.CreateDto(entity);
        }
    }
}
