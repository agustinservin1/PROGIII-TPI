using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class DoctorDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public int SpecialtyId { get; set; }

        public static DoctorDto CreateDoctorDto(Doctor doctor)
        {
            var doctorDto = new DoctorDto()
            {
                Id = doctor.Id,
                Name = doctor.Name,
                LastName = doctor.LastName,
                PhoneNumber = doctor.PhoneNumber,
                DateOfBirth = doctor.DateOfBirth,
                SpecialtyId = doctor.SpecialtyId,
                Email = doctor.Email,
                Password = doctor.Password,
            };
            return doctorDto;
        }

        public static IEnumerable<DoctorDto> CreatelistDto(IEnumerable<Doctor> doctors)
        {
            List<DoctorDto> listDto = new List<DoctorDto>();
            foreach (var doctor in doctors)
            {
                listDto.Add(CreateDoctorDto(doctor));
            }
            return listDto;
        }
    }
}
