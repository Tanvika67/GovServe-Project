using GovServe_Project.DTOs.Admin;
using GovServe_Project.Services.Interfaces.AdminServiceInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GovServe_Project.Controllers.AdminController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IServiceService _service;

        public ServicesController(IServiceService service)
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
       // [Authorize(Roles = "Admin,Citizen,Officer")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _service.GetByIdAsync(id));
        }

        [HttpGet("active")]
        //[Authorize(Roles = "Admin,Citizen,Officer")]
        public async Task<IActionResult> GetActiveServices()
        {
            return Ok(await _service.GetActiveAsync());
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(ServiceDTO dto)
        {
            return Ok(await _service.CreateAsync(dto));
        }

        [HttpPut("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, ServiceDTO dto)
        {
            return Ok(await _service.UpdateAsync(id, dto));
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return Ok("Deleted successfully.");
        }

        [HttpGet("search")]
        //[Authorize(Roles = "Admin,Officer")]
        public async Task<IActionResult> SearchByDepartment([FromQuery] string departmentName)
        {
            return Ok(await _service.SearchByDepartmentAsync(departmentName));
        }

        [HttpGet("count/summary")]
        public async Task<IActionResult> GetActiveVsTotal()
        {
            var result = await _service.GetActiveVsTotalAsync();
            return Ok(result);
        }
    }
}