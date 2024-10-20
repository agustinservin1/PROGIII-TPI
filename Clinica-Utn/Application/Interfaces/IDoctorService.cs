using Application.Models;
using Application.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IDoctorService
    {
        DoctorDto GetById(int id);
        IEnumerable<DoctorDto> GetAll();
        DoctorDto CreateDoctor(DoctorCreateRequest doctor);
        DoctorDto UpdateDoctor(int id, DoctorUpdateRequest doctor);
        DoctorDto DeleteDoctor(int id);
        IEnumerable<DoctorDto> GetBySpecialty(int id);


    }
}
