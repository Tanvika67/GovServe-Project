using GovServe_Project.DTOs;
using GovServe_Project.DTOs.Admin;
using GovServe_Project.Services;
using GovServe_Project.Services_Interfaces_AdminServiceInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GovServe_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkflowStagesController : ControllerBase
    {
        private readonly IWorkflowStageService _service;

        public WorkflowStagesController(IWorkflowStageService service)
        {
            _service = service;
        }

        [HttpGet]
        //[Authorize(Roles = "Admin,Supervisor,Officer")]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("service/{serviceId}")]
        //[Authorize(Roles = "Admin,Supervisor,Officer")]
        public async Task<IActionResult> GetByService(int serviceId)
            => Ok(await _service.GetByServiceAsync(serviceId));

        [HttpGet("{id}")]
       // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get(int id)
            => Ok(await _service.GetByIdAsync(id));

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(WorkflowStageCreateDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = result.StageID }, result);
        }

        [HttpPut("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, WorkflowStageCreateDto dto)
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

        // Reassign endpoint
        [HttpPut("{id}/reassign")]
        //[Authorize(Roles = "Admin")]

        public async Task<IActionResult> Reassign(int id, WorkflowStageReassignDto dto)
        {
            var result = await _service.ReassignAsync(id, dto);
            return Ok(result);
        }
    }
}
  