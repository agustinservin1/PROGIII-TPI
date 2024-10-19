using Domain.Entities;
using Domain.InterFaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class DoctorRepository : BaseRepository<Doctor>, IDoctorRepository
    {
        private readonly ApplicationContext _context;

        public DoctorRepository(ApplicationContext context) : base (context)
        {
            _context = context;
        }
        public IEnumerable<Doctor> GetDoctorsBySpecialty(int id)
        {
            var doctors = _context.Doctors.Where(p => p.SpecialtyId == id)
                                            .ToList();

            return doctors;
        }
    }
}
