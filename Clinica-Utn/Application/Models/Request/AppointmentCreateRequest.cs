using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Request
{
    public class AppointmentCreateRequest
    {
        public int DoctorId { get; set; }
        public int? PatientId { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; } = string.Empty;
        public AppointmentStatus Status { get; set; }
    }
}
