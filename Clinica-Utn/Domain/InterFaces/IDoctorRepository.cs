using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.InterFaces
{
    public interface IDoctorRepository : IBaseRepository<Doctor>
    {
        IEnumerable<Doctor> GetDoctorsBySpecialty(int id);
    }
}
