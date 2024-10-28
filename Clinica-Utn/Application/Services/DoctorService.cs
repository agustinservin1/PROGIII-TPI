using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Domain.Entities;
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
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly ISpecialtyRepository _specialtyRepository;
        private readonly IUserRepository _userRepository;
        public DoctorService(IDoctorRepository doctorRepository, ISpecialtyRepository specialtyRepository, IUserRepository userRepository)
        {
            _doctorRepository = doctorRepository;
            _specialtyRepository = specialtyRepository;
            _userRepository = userRepository;
        }

        public DoctorDto GetById(int id)
        {
            var doctor = _doctorRepository.GetById(id);
            if (doctor == null) 
            {
                throw new NotFoundException($"No se encontro el medico con el id {id}");
            }
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
                throw new NotFoundException($"No se encontro la especialidad con el id {doctor.SpecialtyId}");
            }

            if(specialty.Status == false)
            {
                throw new NotFoundException($"Esta especialidad no se encuentra disponible en este momento");
            }

            var emailValidate = _userRepository.ValidateEmail(doctor.Email);
            if (emailValidate != null)
            {
                throw new NotFoundException($"Ya existe un usuario registrado con este email {doctor.Email}");
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

            if (entity == null) 
            {
                throw new NotFoundException($"No se encontro el medico con el id{id}");
            }
            if (specialty == null)
            {
                throw new NotFoundException($"No se encontro especialidad con el id {doctor.SpecialtyId}.");
            }
            var emailValidate = _userRepository.ValidateEmail(doctor.Email);
            if (emailValidate != null)
            {
                throw new NotFoundException($"Ya existe un usuario registrado con este email {doctor.Email}");
            }

            entity.Name = doctor.Name;
            entity.LastName = doctor.LastName;
            entity.PhoneNumber = doctor.PhoneNumber;
            entity.DateOfBirth = doctor.DateOfBirth;
            entity.SpecialtyId = doctor.SpecialtyId;

            var newEntity = _doctorRepository.Update(entity);
            return DoctorDto.CreateDoctorDto(newEntity);
            
        }
        public DoctorDto DeleteDoctor(int id)
        {
            var doctor = _doctorRepository.GetById(id);
            if (doctor == null)
            {
                throw new NotFoundException($"No se encontro doctor con el id {id}");
            }
            var entity = _doctorRepository.Delete(doctor);
            return DoctorDto.CreateDoctorDto(entity);

        }
        public IEnumerable<DoctorDto> GetBySpecialty(int id)
        {
            var specialty = _specialtyRepository.GetById(id);
            if (specialty == null)
            {
                throw new NotFoundException($"No se encontro especialidad con el id {id}.");
            }

            var listDoctor = _doctorRepository.GetDoctorsBySpecialty(id);
            return DoctorDto.CreatelistDto(listDoctor);
        }

    }
}
