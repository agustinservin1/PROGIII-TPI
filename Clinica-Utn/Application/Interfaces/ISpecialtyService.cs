using Application.Models.Request;
using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISpecialtyService
    {
        SpecialtyDto GetSpecialtyById(int id);
        IEnumerable<SpecialtyDto> GetAllSpecialties();
        SpecialtyDto CreateSpecialty(SpecialtyForRequest specialty);
        public string DeleteSpecialty(int id);


    }
}
