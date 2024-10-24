using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.InterFaces
{
    public interface IAppointmentRepository : IBaseRepository<Appointment>
    {
        Appointment? GetAppointmentByWithPatientAndDoctor(int id);
        IEnumerable<Appointment> GetAppointmentByPatientId(int patientId);
        IEnumerable<Appointment> GetAppointmentByDoctorId(int doctorId);
        IEnumerable<Appointment> GetByAvailable(int id);
    }

}
