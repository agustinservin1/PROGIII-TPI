using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Patient : User

    {
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public Address Address { get; set; }

        public Patient()
        {
            UserRole = UserRole.Patient;
        }
    }
}
