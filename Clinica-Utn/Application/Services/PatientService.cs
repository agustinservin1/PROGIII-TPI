using Application.Models;
using Application.Models.Request;
using Domain.Entities;
using Domain.InterFaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PatientService
    {
        private readonly IPatientRepository _repository;

        public PatientService(IPatientRepository patientRepository)
        {
            _repository = patientRepository;
        }

        public PatientDto? GetPatientById(int id)
        {
            var Patient = _repository.GetById(id);
            if (Patient != null)
            {
                return PatientDto.CreatePatient(Patient);

            }

            throw new ArgumentException("failled");

        }
        public PatientDto? GetPatientByIdWithAddress(int id)
        {
            var patient = _repository.GetByIdIncludeAddress(id);

            if (patient != null)
            {
                return PatientDto.CreatePatient(patient);

            }

            throw new ArgumentException("failled");
        }
            public PatientDto CreatePatient(PatientCreateRequest patient)
            { 
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

       

       

    }
}
