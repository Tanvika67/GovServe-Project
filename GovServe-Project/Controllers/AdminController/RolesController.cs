using GovServe_Project.DTOs;
using GovServe_Project.DTOs.AdminDTO;
using GovServe_Project.Services.Interfaces.AdminServiceInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GovServe_Project.Controllers.AdminController
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _service;

        public RolesController(IRoleService service)
        {
            _service = service;
        }

        [HttpGet]
        //[Authorize(Roles = "Admin,Citizen,Officer,Supervisor,Grievance")]
        public async Task<IActionResult> GetAll()
            => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin,Citizen,Officer,Supervisor,Grievance")]    
        public async Task<IActionResult> Get(int id)
            => Ok(await _service.GetByIdAsync(id));

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(RoleCreateDto dto)
        {
            var result = await _service.CreateAsync(dto);

            return CreatedAtAction(nameof(Get),
                new { id = result.RoleID }, result);
        }

        [HttpPut("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, RoleCreateDto dto)
        {
            await _service.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }


}
