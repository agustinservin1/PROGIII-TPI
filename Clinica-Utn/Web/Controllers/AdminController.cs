using Application.Interfaces;
using Application.Models.Request;
using Application.Services;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("GetById/{id}")]
        public IActionResult GetAdminById(int id)
        {
            var result = _adminService.GetById(id);
            return Ok(result);
        }

        [HttpGet("GetAll")]
        public IActionResult GetAllAdmins()
        {
            var admins = _adminService.GetAll();
            return Ok(admins);
        }

        [HttpPost("AddAdmin")]
        //[Authorize(Policy = "Admin")]
        public IActionResult AddAdmin([FromBody] AdminCreateRequest request)
        {
            var newObj = _adminService.CreateAdminDto(request);
            return CreatedAtAction(nameof(GetAdminById), new { id = newObj.Id }, newObj);
        }

        [HttpPut("Update/{id}")]
        [Authorize(Policy = "Admin")]
        public IActionResult UpdateAdmin(int id, [FromBody] UpdateAdminForRequest request)
        {
            _adminService.UpdateDto(id, request);
            return NoContent();
        }

        [HttpPut("ChangeStatusAdmin/{id}")]
        [Authorize(Policy = "Admin")]
        public IActionResult ChangeStatus(int id)
        {
            _adminService.ChangeStatus(id);
            return NoContent();
        }

        [HttpDelete("Delete/{id}")]
        [Authorize(Policy = "Admin")]
        public IActionResult DeleteAdmin(int id)
        {
            _adminService.DeleteAdminDto(id);
            return NoContent();
        }
    }
}