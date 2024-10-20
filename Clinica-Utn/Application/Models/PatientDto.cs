using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class PatientDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public UserRole UserRole { get; set; }
        public AddressDto? AddressDto { get; set; } = new AddressDto();


        public static PatientDto CreatePatient(Patient patient)
        {

            var newDto = new PatientDto()
            {
                Id = patient.Id,
                Name = patient.Name,
                LastName = patient.LastName,
                PhoneNumber = patient.PhoneNumber,
                DateOfBirth = patient.DateOfBirth,
                Email = patient.Email,
                Password = patient.Password,
                UserRole = patient.UserRole,
                AddressDto = AddressDto.CreateAddressDto(patient.Address)
            };
            return newDto;
        }

        public static IEnumerable<PatientDto> CreateList(IEnumerable<Patient> patients)
        {
            List<PatientDto> listDto = new List<PatientDto>();
            foreach (var s in patients)
            {
                listDto.Add(CreatePatient(s));
            }
            return listDto;
        }


    }
}
