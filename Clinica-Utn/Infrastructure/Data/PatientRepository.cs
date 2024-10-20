using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class PatientRepository : BaseRepository<Patient>
    {
        private readonly ApplicationContext _repository;

        public PatientRepository(ApplicationContext repository) : base(repository)
        {
            _repository = repository;
        }
        public IEnumerable<Patient> GetAllPatientWithAddress()
        {
            var list = _repository.Patients
                                   .Include(a => a.Address)
                                   .ToList();
            return list;
        }
        public Patient? GetByIdIncludeAddress(int id)
        {
            var entity = _repository.Patients.Include(a => a.Address)
                                             .FirstOrDefault(c => c.Id == id);
            return entity;
        }
    }
}
