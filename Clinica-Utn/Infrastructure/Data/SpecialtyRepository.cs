using Domain.Entities;
using Domain.InterFaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class SpecialtyRepository : BaseRepository<Specialty>, ISpecialtyRepository

    {
        private readonly ApplicationContext _context;

        public SpecialtyRepository(ApplicationContext context) : base(context)
        {
           _context = context;
        }

    }
}
