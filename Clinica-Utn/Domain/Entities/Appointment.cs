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
    public class Appointment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        //public int DoctorId { get; set; }
        //public int? PatientId { get; set; } //Agregue el int nulleable para que me permita usar el crear el turno sin un paciente.
        public Doctor Doctor { get; set; }
        public Patient? Patient { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public AppointmentStatus Status { get; set; }
    }
}
