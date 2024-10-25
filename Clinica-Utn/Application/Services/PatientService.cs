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
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _repository;
        private readonly IUserRepository _userRepository;

        public PatientService(IPatientRepository patientRepository, IUserRepository userRepository)
        {
            _repository = patientRepository;
            _userRepository = userRepository;
        }

        public PatientDto? GetPatientByIdWithAddress(int id)
        {
            var patient = _repository.GetByIdIncludeAddress(id);

            if (patient == null)
            {
                throw new NotFoundException($"No se encontró el paciente con el id {id}");
            }
            return PatientDto.CreatePatient(patient);
        }

        public IEnumerable<PatientDto> GetPatientsWithAddress() 
        {
            var list = _repository.GetAllPatientWithAddress();
            return PatientDto.CreateList(list);
        }

        public PatientDto CreatePatient(PatientCreateRequest patient)
        {
            var emailValidate = _userRepository.ValidateEmail(patient.Email);
            if (emailValidate != null)
            {
                throw new NotFoundException($"Ya existe un usuario registrado con este email {patient.Email}");
            }
            var newAdress = new Address()
                {
                    Street = patient.Address.Street,
                    PostalCode = patient.Address.PostalCode,
                    City = patient.Address.City,
                    Province = patient.Address.Province,
                };

                var entity = new Patient()
                {
                    Name = patient.Name,
                    LastName = patient.LastName,
                    PhoneNumber = patient.PhoneNumber,
                    DateOfBirth = patient.DateOfBirth,
                    Address = newAdress,
                    Email = patient.Email,
                    Password = patient.Password,
                };

                var newEntity = _repository.Create(entity);
                return PatientDto.CreatePatient(newEntity);
        }

        public PatientDto UpdatePatient(int id, PatientUpdateForRequest patient)
        {
            var entity = _repository.GetByIdIncludeAddress(id);

            if (entity == null)
            {
                throw new NotFoundException($"No se encontró el paciente con el id {id}");
            }
            
            if (patient.Address == null)
            {
                throw new NotFoundException($"No se encontró la direccion con el id {id}");
            }
            var emailValidate = _userRepository.ValidateEmail(patient.Email);
            if (emailValidate != null)
            {
                throw new NotFoundException($"Ya existe un usuario registrado con este email {patient.Email}");
            }

            entity.Name = patient.Name;
                entity.LastName = patient.LastName;
                entity.PhoneNumber = patient.PhoneNumber;
                entity.DateOfBirth = patient.DateOfBirth;
                entity.Address.City = patient.Address.City;
                entity.Address.Street = patient.Address.Street;
                entity.Address.Province = patient.Address.Province;
                entity.Address.PostalCode = patient.Address.PostalCode;

               
                var newEntity = _repository.Update(entity);
                return PatientDto.CreatePatient(newEntity);
           
        }

        public PatientDto DeletePatient(int id)
        {

            var entity = _repository.GetByIdIncludeAddress(id);

            if (entity == null)
            {
                throw new NotFoundException($"No se encontró el paciente con el id {id}");
            }
            var patient = _repository.Delete(entity);
            return PatientDto.CreatePatient(patient);

        }
    }
}
