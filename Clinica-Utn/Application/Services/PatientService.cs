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
            var Patient = _repository.GetByIdIncludeAddress(id);
            if (Patient != null)
            {
                return PatientDto.CreatePatient(Patient);

            }

            throw new ArgumentException("failled");

        }

    }
}
