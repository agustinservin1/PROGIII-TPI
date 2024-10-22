using Application.Interfaces;
using Application.Models.Request;
using Application.Services;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        [HttpGet("/GetAdminById/{id}")]
        public IActionResult GetAdminById(int id)
        {
            try
            {
                var result = _adminService.GetById(id);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }

             }
        [HttpGet("/GetAllAdmins")]
        public IActionResult GetAllAdmins()
        {
            try
            {
                var admins = _adminService.GetAll();
                return Ok(admins);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost("/AddAdmin")]
        public IActionResult AddAdmin([FromBody] AdminCreateRequest request)
        {
            var newObj = _adminService.CreateAdminDto(request);
            return CreatedAtAction(nameof(GetAdminById), new { id = newObj.Id }, newObj);
        }
        [HttpPut("/UpdateAdmin/{id}")]
        public IActionResult UpdateAdmin(int id, [FromBody] UpdateAdminForRequest request)
        {
            try
            {
                _adminService.UpdateDto(id, request);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("/DeleteAdmin/{id}")]

        public IActionResult DeleteAdmin(int id)
        {
            try
            {
                _adminService.DeleteAdminDto(id);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("/ChangeStatusAdmin/{id}")]
        public IActionResult ChangeStatus(int id)
        {
            _adminService.ChangeStatus(id);
            return NoContent();
        }
    }
}