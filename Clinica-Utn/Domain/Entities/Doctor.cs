using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Doctor : User

    {
        public Specialty Specialty { get; set; }

        [ForeignKey("Specialty")]
        public int SpecialtyId { get; set; }
        public ICollection<Appointment> AssignedAppointment { get; set; } = new List<Appointment>();

        public Doctor()
        {
            UserRole = UserRole.Doctor;
        }
    }
}
