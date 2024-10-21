using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Request
{
    public class AppointmentAssignForRequest
    {
        public int IdAppointment { get; set; }
        public int IdPatient { get; set; }
    }
}
