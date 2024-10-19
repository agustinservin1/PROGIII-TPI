using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Doctor : User

    {
        public Specialty Specialty { get; set; }
        //public int SpecialtyId { get; set; }
        public ICollection<Appointment> AssignedAppointment { get; set; } = new List<Appointment>();

        public Doctor()
        {
            UserRole = UserRole.Doctor;
        }
    }
}
