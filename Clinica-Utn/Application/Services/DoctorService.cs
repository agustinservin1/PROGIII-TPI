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
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;
        public DoctorService(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public DoctorDto GetById(int id)
        {
            var doctor = _doctorRepository.GetById(id);
            return DoctorDto.CreateDoctorDto(doctor);
        }
        public IEnumerable<DoctorDto> GetAll()
        {
            var list = _doctorRepository.GetAll();
            return DoctorDto.CreatelistDto(list);
        }
    }
}
