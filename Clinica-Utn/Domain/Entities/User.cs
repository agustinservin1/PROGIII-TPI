using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public abstract class User

    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [MaxLength(20)]
        public string Password { get; set; } = string.Empty;
        public UserRole UserRole { get; set; }
        public bool IsAvailable { get; set; }
        public User()
        {
            IsAvailable = true;
        }
    }
}
