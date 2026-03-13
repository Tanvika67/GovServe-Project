using GovServe_Project.DTOs.Admin;
using GovServe_Project.Services.Interfaces.AdminServiceInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GovServe_Project.Controllers.AdminController
{
    [Route("api/[controller]")]
    [ApiController]
    public class EligibilityRulesController : ControllerBase
    {
        private readonly IEligibilityRuleService _service;

        public EligibilityRulesController(IEligibilityRuleService service)
        {
            _service = service;
        }

        [HttpGet]
        //[Authorize(Roles = "Admin,Citizen,Officer")]
        public async Task<IActionResult> GetAll()
        {
            
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin,Citizen,Officer")]
        public async Task<IActionResult> GetById(int id)
        {
           
            return Ok(await _service.GetByIdAsync(id));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(EligibilityRuleDTO dto)
        {
            
            return Ok(await _service.CreateAsync(dto));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, EligibilityRuleDTO dto)
        {
     
            return Ok(await _service.UpdateAsync(id, dto));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return Ok("Deleted successfully.");
        }

        [HttpGet("search")]
        [Authorize(Roles = "Admin,Citizen,Officer")]
        public async Task<IActionResult> SearchByServiceName([FromQuery] string serviceName)
        {
            return Ok(await _service.SearchByServiceNameAsync(serviceName));
        }

    }
}
