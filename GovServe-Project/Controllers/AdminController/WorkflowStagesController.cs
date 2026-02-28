using GovServe_Project.DTOs;
using GovServe_Project.DTOs.Admin.GovServe_Project.DTOs;
using GovServe_Project.Services;
using GovServe_Project.Services_Interfaces_AdminServiceInterface;
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
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("service/{serviceId}")]
        public async Task<IActionResult> GetByService(int serviceId)
            => Ok(await _service.GetByServiceAsync(serviceId));

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
            => Ok(await _service.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create(WorkflowStageCreateDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = result.StageID }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, WorkflowStageCreateDto dto)
        {
            await _service.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }

        // Reassign endpoint
        [HttpPut("{id}/reassign")]
        public async Task<IActionResult> Reassign(int id, WorkflowStageReassignDto dto)
        {
            var result = await _service.ReassignAsync(id, dto);
            return Ok(result);
        }
    }
}
  