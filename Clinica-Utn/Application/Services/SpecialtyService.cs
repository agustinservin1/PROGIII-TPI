using Application.Interfaces;
using Application.Models;
using Domain.InterFaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SpecialtyService : ISpecialtyService
    {
        private readonly ISpecialtyRepository _specialtyRepository;

        public SpecialtyService(ISpecialtyRepository specialtyRepository)
        {
            _specialtyRepository = specialtyRepository;
        }

        public SpecialtyDto GetSpecialtyById(int id)
        {
            var specialty = _specialtyRepository.GetById(id);

            return SpecialtyDto.CreateSpecialtyDto(specialty);
        }
        public IEnumerable<SpecialtyDto> GetAllSpecialties()
        {
            var list = _specialtyRepository.GetAll();
            return SpecialtyDto.CreateListDto(list);
        }
    }
}
