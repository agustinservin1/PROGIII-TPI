using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Domain.Entities;
using Domain.Exceptions;
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
        private readonly IDoctorRepository _doctorRepository;

        public SpecialtyService(ISpecialtyRepository specialtyRepository, IDoctorRepository doctorRepository)
        {
            _specialtyRepository = specialtyRepository;
            _doctorRepository = doctorRepository;
        }

        public SpecialtyDto GetSpecialtyById(int id)
        {
            var specialty = _specialtyRepository.GetById(id);
            if (specialty == null) 
            {
                throw new ArgumentException($"No se encontro la especialidad con el id {id}");
            }
            return SpecialtyDto.CreateSpecialtyDto(specialty);
        }
        public IEnumerable<SpecialtyDto> GetAllSpecialties()
        {
            var list = _specialtyRepository.GetAll();
            return SpecialtyDto.CreateListDto(list);
        }
        public SpecialtyDto CreateSpecialty(SpecialtyForRequest specialty)
        {
            var entity = new Specialty()
            {
                Name = specialty.Name,
                Description = specialty.Description
            };

            var newSpecialty = _specialtyRepository.Create(entity);
            return SpecialtyDto.CreateSpecialtyDto(newSpecialty);
        }
        public string DeleteSpecialty(int id)
        {
            var specialty = _specialtyRepository.GetById(id);
            if(specialty == null)
            {
                throw new NotFoundException($"No existe ninguna especialidad con el id {id}");
            }
            var list = _doctorRepository.GetDoctorsBySpecialty(id);

            if (list.Any())
            {
                specialty.Status = false;
                _specialtyRepository.Update(specialty);
                return "La especialidad no se va a poder eliminar porque tiene asociado doctores. Se aplicara un delete logico";
            }
            else
            {
                _specialtyRepository.Delete(specialty);
                return "Eliminacion con exito";

            }
        }
    }
}
