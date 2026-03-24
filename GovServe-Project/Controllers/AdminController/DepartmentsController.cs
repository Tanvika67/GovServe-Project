using GovServe_Project.DTOs.Admin;
using GovServe_Project.Services.Interfaces.AdminServiceInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GovServe_Project.Controllers.AdminController
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _service;

        public DepartmentController(IDepartmentService service)
        {
            _service = service;
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Supervisor,Officer")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _service.GetByIdAsync(id));
        }

        [HttpGet("active")]
        //[Authorize(Roles = "Admin,Supervisor,Officer")]
        public async Task<IActionResult> GetActiveDepartments()
        {
            return Ok(await _service.GetActiveAsync());
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(DepartmentDTO dto)
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.DepartmentID }, result);
        }

        [HttpPut("{id}")]
       // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, DepartmentDTO dto)
        {
            return Ok(await _service.UpdateAsync(id, dto));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return Ok("Deleted Successfully");
        }

        [HttpGet("count")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetTotalCountAsync()
        {
            var count = await _service.GetTotalCountAsync();
            return Ok(count);
        }
    }
}