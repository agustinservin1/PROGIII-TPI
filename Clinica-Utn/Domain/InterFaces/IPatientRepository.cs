using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.InterFaces
{
    public interface IPatientRepository : IBaseRepository<Patient>
    {
        Patient? GetByIdIncludeAddress(int id);
        IEnumerable<Patient> GetAllPatientWithAddress();
    }
}
