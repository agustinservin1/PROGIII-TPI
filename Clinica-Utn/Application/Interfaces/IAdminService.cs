using Application.Models;
using Application.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAdminService
    {
        AdminDto GetById(int id);
        IEnumerable<AdminDto> GetAll();
        AdminDto CreateAdminDto(AdminCreateRequest admin);
        AdminDto UpdateDto(int id, UpdateAdminForRequest admin);
        AdminDto DeleteAdminDto(int id);
    }
}
