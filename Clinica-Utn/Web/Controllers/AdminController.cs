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
                var result = _adminService.GetById(id);
                return Ok(result);

             }
        [HttpGet("/GetAllAdmins")]
        public IActionResult GetAllAdmins()
        {
                var admins = _adminService.GetAll();
                return Ok(admins);
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
                _adminService.UpdateDto(id, request);
                return NoContent();
        }

        [HttpDelete("/DeleteAdmin/{id}")]

        public IActionResult DeleteAdmin(int id)
        {
                _adminService.DeleteAdminDto(id);
                return NoContent();
        }

        [HttpPut("/ChangeStatusAdmin/{id}")]
        public IActionResult ChangeStatus(int id)
        {
            _adminService.ChangeStatus(id);
            return NoContent();
        }
    }
}