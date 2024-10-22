using Domain.Entities;
using Domain.InterFaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class AdminRepository : BaseRepository<Admin> ,IAdminRepository
    {
        private readonly ApplicationContext _repository;

        public AdminRepository(ApplicationContext repository) : base(repository) 
        {
            _repository = repository;
        }


    }
}
