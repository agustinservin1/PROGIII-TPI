using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class AdminDto
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

        public static AdminDto CreateAdminDto(Admin admin)
        {
            var adminDto = new AdminDto()
            {
                Id = admin.Id,
                Name = admin.Name,
                LastName = admin.LastName,
                PhoneNumber = admin.PhoneNumber,
                DateOfBirth = admin.DateOfBirth,
                Email = admin.Email,
                Password = admin.Password,
            };
            return adminDto;
        }

        public static IEnumerable<AdminDto> CreatelistDto(IEnumerable<Admin> admins)
        {
            List<AdminDto> listDto = new List<AdminDto>();
            foreach (var admin in admins)
            {
                listDto.Add(CreateDoctorDto(admin));
            }
            return listDto;
        }
    }
}
