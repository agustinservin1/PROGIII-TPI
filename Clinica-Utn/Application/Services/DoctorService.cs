using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Domain.Entities;
using Domain.Exceptions;
using Domain.InterFaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly ISpecialtyRepository _specialtyRepository;
        public DoctorService(IDoctorRepository doctorRepository, ISpecialtyRepository specialtyRepository)
        {
            _doctorRepository = doctorRepository;
            _specialtyRepository = specialtyRepository;
        }

        public DoctorDto GetById(int id)
        {
            var doctor = _doctorRepository.GetById(id);
            return DoctorDto.CreateDoctorDto(doctor);
        }
        public IEnumerable<DoctorDto> GetAll()
        {
            var list = _doctorRepository.GetAll();
            return DoctorDto.CreatelistDto(list);
        }
        public DoctorDto CreateDoctor(DoctorCreateRequest doctor)
        {

            var specialty = _specialtyRepository.GetById(doctor.SpecialtyId);

            if (specialty == null)
            {
                throw new NotFoundException("No se encontro especialidad.");
            }
            var entity = new Doctor()
            {
                Name = doctor.Name,
                LastName = doctor.LastName,
                PhoneNumber = doctor.PhoneNumber,
                DateOfBirth = doctor.DateOfBirth,
                SpecialtyId = doctor.SpecialtyId,
                Email = doctor.Email,
                Password = doctor.Password
            };


            var doctorEntity = _doctorRepository.Create(entity);
            return DoctorDto.CreateDoctorDto(doctorEntity);
        }
        public DoctorDto UpdateDoctor(int id, DoctorUpdateRequest doctor)
        {
            var entity = _doctorRepository.GetById(id);
            var specialty = _specialtyRepository.GetById(doctor.SpecialtyId);
            if (specialty == null)
            {
                throw new NotFoundException("No se encontro especialidad.");
            }

            if (entity != null)
            {
                entity.Name = doctor.Name;
                entity.LastName = doctor.LastName;
                entity.PhoneNumber = doctor.PhoneNumber;
                entity.DateOfBirth = doctor.DateOfBirth;
                entity.SpecialtyId = doctor.SpecialtyId;


                var newEntity = _doctorRepository.Update(entity);
                return DoctorDto.CreateDoctorDto(newEntity);
            }

            throw new ArgumentException("All fields are required.");
        }
        public DoctorDto DeleteDoctor(int id)
        {
            var doctor = _doctorRepository.GetById(id);
            if (doctor == null)
            {
                throw new NotFoundException("No se encontro doctor.");
            }
            var entity = _doctorRepository.Delete(doctor);
            return DoctorDto.CreateDoctorDto(entity);

        }
        public IEnumerable<DoctorDto> GetBySpecialty(int id)
        {
            var listDoctor = _doctorRepository.GetDoctorsBySpecialty(id);
            if (listDoctor == null)
            {
                throw new NotFoundException("No se encontro especialidad.");
            }

            return DoctorDto.CreatelistDto(listDoctor);
        }

    }
}
