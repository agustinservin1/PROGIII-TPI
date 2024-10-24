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
    public class AdminService: IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }
        
        public AdminDto GetById(int id)
        {
            var admin = _adminRepository.GetById(id);

            if (admin == null)
            {
                throw new NotFoundException($"No se encontró el Admin con el id {id}");
            }
            return AdminDto.CreateAdminDto(admin);
        }

        public IEnumerable<AdminDto> GetAll() 
        {
            var admin = _adminRepository.GetAll();
            return AdminDto.CreatelistDto(admin);
        }

        public AdminDto CreateAdminDto(AdminCreateRequest admin) 
        {
            var entity = new Admin()
            {
                Name = admin.Name,
                LastName = admin.LastName,
                Email = admin.Email,
                Password = admin.Password,
                DateOfBirth = admin.DateOfBirth,
                PhoneNumber = admin.PhoneNumber,
            };
            _adminRepository.Create(entity);
            return AdminDto.CreateAdminDto(entity);
        }

        public AdminDto UpdateDto(int id,UpdateAdminForRequest admin) 
        {
            var entity = _adminRepository.GetById(id);

            if (entity == null)
            {
                throw new NotFoundException($"No se encontró el Admin con el id {id}");
            }

            entity.Name = admin.Name;
            entity.LastName = admin.LastName;
            entity.Email = admin.Email;
            entity.PhoneNumber = admin.PhoneNumber;
            entity.DateOfBirth = admin.DateOfBirth;

            _adminRepository.Update(entity);
            return AdminDto.CreateAdminDto(entity);

        }
        public AdminDto DeleteAdminDto(int id) 
        {
            var entity = _adminRepository.GetById(id);

            if (entity == null)
            {
                throw new NotFoundException($"No se encontró el Admin con el id {id}");
            }
            _adminRepository.Delete(entity);
            return AdminDto.CreateAdminDto(entity);
        }

        public AdminDto ChangeStatus(int id)
        {
            var entity = _adminRepository.GetById(id);

            if (entity == null)
            {
                throw new NotFoundException($"No se encontró el Admin con el id {id}");
            }

            entity.IsAvailable = false;
            _adminRepository.Update(entity);

            return AdminDto.CreateAdminDto(entity);
        }
    }
}
